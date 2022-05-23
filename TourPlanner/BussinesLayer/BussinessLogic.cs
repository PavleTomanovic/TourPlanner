using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using TourPlanner.DataAccessLayer;
using TourPlanner.Documents;
using TourPlanner.DTO;
using TourPlanner.Models;
using TourPlanner.Util;

namespace TourPlanner.BussinesLayer
{
    public class BussinessLogic
    {
        private static BussinessLogic logicInstance = new BussinessLogic();

        public bool CreateRoute(string name, string from, string to, string transport, string comment)
        {
            List<TourPreview> routeNames = SelectTourNameId();

            if (routeNames.Any(n => n.tourName == name)) //routeNames.Contains(name)
            {
                return false;
            }
            else
            {
                HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;
                HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

                httpDTO.From = from;
                httpDTO.To = to;

                httpResponseDTO = HttpRequest.Instance.GetRoutes(httpDTO);
                httpResponseDTO.Route.ImageUrl = HttpRequest.Instance.GetRouteImage(httpDTO);

                httpResponseDTO.Route.Name = name;
                httpResponseDTO.Route.Comment = comment;
                httpResponseDTO.Route.From = from;
                httpResponseDTO.Route.To = to;
                httpResponseDTO.Route.Transport = transport;

                DatabaseConnection.Instance.ExecuteInsertRoute(BussinessFactory.Instance.SqlDTO.Insert, httpResponseDTO);

                return true;
            }
        }

        public bool ModifyRoute(string from, string to, string name, string transport, string comment, string routeId)
        {
            try
            {
                HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;
                HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

                httpDTO.From = from;
                httpDTO.To = to;
                httpResponseDTO = HttpRequest.Instance.GetRoutes(httpDTO);
                httpResponseDTO.Route.ImageUrl = HttpRequest.Instance.GetRouteImage(httpDTO);
                httpResponseDTO.Route.Name = name;
                httpResponseDTO.Route.Comment = comment;
                httpResponseDTO.Route.From = from;
                httpResponseDTO.Route.To = to;
                httpResponseDTO.Route.Transport = transport;
                httpResponseDTO.Route.Id = routeId;

                DatabaseConnection.Instance.ExecuteUpdateRoute(BussinessFactory.Instance.SqlDTO.Update, httpResponseDTO);
                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return false;
            }

        }

        public void DeleteRoute(string routeId)
        {
            DatabaseConnection.Instance.ExecuteDeleteRoute(BussinessFactory.Instance.SqlDTO.Delete, routeId);
        }

        public bool CreateLog(string comment, string difficulty, string totalTime, string rating, string routeId)
        {
            try
            {
                TourLogDTO tourLogDTO = new TourLogDTO();

                tourLogDTO.DateTime = DateTime.Now.ToString();
                tourLogDTO.Comment = comment;
                tourLogDTO.Difficulty = difficulty;
                tourLogDTO.TotalTime = totalTime;
                tourLogDTO.Rating = rating;

                DatabaseConnection.Instance.ExecuteInsertLog(BussinessFactory.Instance.SqlDTO.InsertLog, tourLogDTO, routeId);

                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        public bool ModifyLog(string comment, string difficulty, string totalTime, string rating, string logId)
        {
            try
            {
                TourLogDTO tourLogDTO = new TourLogDTO();

                tourLogDTO.Comment = comment;
                tourLogDTO.Difficulty = difficulty;
                tourLogDTO.TotalTime = totalTime;
                tourLogDTO.Rating = rating;
                tourLogDTO.LogId = logId;

                DatabaseConnection.Instance.ExecuteModifyLog(BussinessFactory.Instance.SqlDTO.UpdateLog, tourLogDTO);

                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        public void Deletelog(string logId)
        {
            DatabaseConnection.Instance.ExecuteDeleteLog(BussinessFactory.Instance.SqlDTO.DeleteLog, logId);
        }

        public List<TourPreview> SelectTourNameId()
        {
            List<TourPreview> routeNameId = new List<TourPreview>();

            try
            {
                DataTable dataTable = DatabaseConnection.Instance.ExecuteSelectAllRoutes(BussinessFactory.Instance.SqlDTO.SelectAllRoutes);
                foreach (DataRow row in dataTable.Rows)
                {
                    try
                    {
                        //Check what is better here: TourName or TourId  => bouth :)
                        string routeName = row["TourName"].ToString();
                        string routeId = row["TourId"].ToString();
                        routeNameId.Add(new TourPreview { tourName = routeName, tourId = routeId });
                    }
                    catch (Exception e)
                    {
                        LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                    }
                }
                return routeNameId;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return routeNameId;
            }
        }

        public bool CreateRouteReport(string routeId)
        {
            try
            {
                HttpResponseDTO httpResponseDTO = SelectAllFromRoute(routeId);
                DocumentCreation.Instance.RouteReportCreation(httpResponseDTO);
                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        public bool CreateSummarizeReport(string routeId)
        {
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);

            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    string distance = row["TourDistance"].ToString();
                    string routeName = row["TourName"].ToString();
                    int avgTime = 0;
                    int avgRating = 0;

                    DataTable dataTableLogs = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
                    int totalRows = dataTableLogs.Rows.Count;

                    foreach (DataRow rowLog in dataTableLogs.Rows)
                    {
                        avgTime += Int32.Parse(row["TotalTime"].ToString());
                        avgRating += Int32.Parse(row["Rating"].ToString());
                    }

                    double finalTime = 0;
                    double finalRating = 0;

                    if (totalRows > 0)
                    {
                        finalTime = avgTime / totalRows;
                        finalRating = avgRating / totalRows;
                    }

                    DocumentCreation.Instance.RouteSummarizeReportCreation(finalTime, finalRating, distance, routeName);
                }
                catch (Exception e)
                {
                    LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                    return false;
                }
            }

            return true;
        }

        public string ImportRouteFromFile(string filename)
        {
            FileInfo file = new FileInfo(filename);

            if (file.Extension != ".xml")
            {
                LoggerToFile.LogError("ImportFromFile: File is not in right format");
                return "badFile";
            }
            else
            {
                HttpResponseDTO httpResponseDTO = ImportExport.Instance.ImportFile(filename);

                List<TourPreview> routeNames = SelectTourNameId();

                if (routeNames.Any(n => n.tourName == httpResponseDTO.Route.Name))
                {
                    LoggerToFile.LogError("ImportFromFile: Route with this name already exsits!");
                    return "nameExists";
                }
                else
                {
                    DatabaseConnection.Instance.ExecuteInsertRoute(BussinessFactory.Instance.SqlDTO.Insert, httpResponseDTO);
                    return "done";
                }
            }
        }

        public HttpResponseDTO SelectAllFromRoute(string routeId)
        {
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

            foreach (DataRow row in dataTable.Rows)
            {
                httpResponseDTO.Route.Id = row["TourId"].ToString();
                httpResponseDTO.Route.Name = row["TourName"].ToString();
                httpResponseDTO.Route.Comment = row["TourComment"].ToString();
                httpResponseDTO.Route.From = row["TourFrom"].ToString();
                httpResponseDTO.Route.To = row["TourTo"].ToString();
                httpResponseDTO.Route.Transport = row["TourTransport"].ToString();
                httpResponseDTO.Route.Distance = row["TourDistance"].ToString();
                httpResponseDTO.Route.Time = row["TourTime"].ToString();
                httpResponseDTO.Route.ImageUrl = row["TourImage"].ToString();
                httpResponseDTO.Route.Favorite = row["TourFavorite"].ToString();
            }

            return httpResponseDTO;
        }

        public bool ExportRouteToFile(string filename, string routeId)
        {
            try
            {
                HttpResponseDTO httpResponseDTO = SelectAllFromRoute(routeId);
                ImportExport.Instance.ExportFile(filename, httpResponseDTO);

                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        public void MakeRouteFavorite(string routeId)
        {
            DatabaseConnection.Instance.ExecuteFavorite(BussinessFactory.Instance.SqlDTO.UpdateFavorite, routeId);
        }

        public string CheckRoutePopularity(string routeId)
        {
            string result;

            DataTable dataTableLogs = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
            int totalRows = dataTableLogs.Rows.Count;

            if (totalRows == 0)
            {
                result = "Not popular route";
            }
            else if (totalRows > 0 && totalRows < 5)
            {
                result = "Popular route";
            }
            else
            {
                result = "Very popular route";
            }

            return result;
        }

        public string CheckRouteChildFriendliness(string routeId)
        {
            string result = "No info";
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);

            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    int distance = Int32.Parse(row["TourDistance"].ToString());
                    int avgTime = 0;
                    int avgDifficulty = 0;

                    DataTable dataTableLogs = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
                    int totalRows = dataTableLogs.Rows.Count;

                    foreach (DataRow rowLog in dataTableLogs.Rows)
                    {
                        avgTime += Int32.Parse(row["TotalTime"].ToString());
                        avgDifficulty += Int32.Parse(row["Difficulty"].ToString());
                    }

                    double finalTime = avgTime / totalRows;
                    double finalDifficulty = avgDifficulty / totalRows;

                    if (distance < 200)
                    {
                        if (finalTime < 120)
                        {
                            if (finalDifficulty < 5)
                            {
                                result = "Child-friendly";
                            }
                            else
                            {
                                result = "Not child-friendly";
                            }
                        }
                        else
                        {
                            result = "Not child-friendly";
                        }
                    }
                    else
                    {
                        result = "Not child-friendly";
                    }
                }
                catch (Exception e)
                {
                    LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                }
            }
            return result;
        }
        public List<string> PrepareTableRouteForSearch(string searchText)
        {
            List<string> result = new List<string>();
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SearchThroughRoutes, searchText);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(row["TourId"].ToString());
            }
            return result;
        }

        public List<TourLogDTO> SelectLogForRoute(string routeId)
        {
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
            TourLogDTO tourLogDTO = new TourLogDTO();
            List<TourLogDTO> list = new List<TourLogDTO>();

            foreach (DataRow row in dataTable.Rows)
            {
                tourLogDTO.DateTime = row["DateTime"].ToString();
                tourLogDTO.Difficulty = row["Difficulty"].ToString();
                tourLogDTO.Comment = row["Comment"].ToString();
                tourLogDTO.Rating = row["Rating"].ToString();
                tourLogDTO.TotalTime = row["TotalTime"].ToString();

                list.Add(tourLogDTO);
            }

            return list;
        }

        public static BussinessLogic LogicInstance
        {
            get
            {
                return logicInstance;
            }
        }
    }
}
