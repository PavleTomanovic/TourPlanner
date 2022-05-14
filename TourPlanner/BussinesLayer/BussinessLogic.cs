using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer;
using TourPlanner.DTO;
using TourPlanner.Util;
using TourPlanner.Documents;

namespace TourPlanner.BussinesLayer
{
    public class BussinessLogic
    {
        private static BussinessLogic logicInstance = new BussinessLogic();

        public void CreateRoute(string from, string to, string name, string description, string transport)
        {
            HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

            httpDTO.From = from;
            httpDTO.To = to;

            httpResponseDTO = HttpRequest.Instance.GetRoutes(httpDTO);
            httpResponseDTO.Route.ImageUrl = HttpRequest.Instance.GetRouteImage(httpDTO);

            httpResponseDTO.Route.Name = name;
            httpResponseDTO.Route.Description = description;
            httpResponseDTO.Route.From = from;
            httpResponseDTO.Route.To = to;
            httpResponseDTO.Route.Transport = transport;

            DatabaseConnection.Instance.ExecuteInsertRoute(BussinessFactory.Instance.SqlDTO.Insert, httpResponseDTO);
        }

        public void ModifyRoute(string from, string to, string name, string description, string transport, string routeId)
        {
            HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

            httpDTO.From = from;
            httpDTO.To = to;

            httpResponseDTO = HttpRequest.Instance.GetRoutes(httpDTO);
            httpResponseDTO.Route.ImageUrl = HttpRequest.Instance.GetRouteImage(httpDTO);

            httpResponseDTO.Route.Name = name;
            httpResponseDTO.Route.Description = description;
            httpResponseDTO.Route.From = from;
            httpResponseDTO.Route.To = to;
            httpResponseDTO.Route.Transport = transport;
            httpResponseDTO.Route.Id = routeId;

            DatabaseConnection.Instance.ExecuteUpdateRoute(BussinessFactory.Instance.SqlDTO.Update, httpResponseDTO);
        }

        public void DeleteRoute(string routeId)
        {
            DatabaseConnection.Instance.ExecuteDeleteRoute(BussinessFactory.Instance.SqlDTO.Delete, routeId);
        }

        public void CreateLog(string comment, string difficulty, string totalTime, string rating, string routeId)
        {
            TourLogDTO tourLogDTO = new TourLogDTO();

            tourLogDTO.DateTime = DateTime.Now.ToString();
            tourLogDTO.Comment = comment;
            tourLogDTO.Difficulty = difficulty;
            tourLogDTO.TotalTime = totalTime;
            tourLogDTO.Rating = rating;

            DatabaseConnection.Instance.ExecuteInsertLog(BussinessFactory.Instance.SqlDTO.InsertLog, tourLogDTO, routeId);
        }

        public void ModifyLog(string comment, string difficulty, string totalTime, string rating, string logId)
        {
            TourLogDTO tourLogDTO = new TourLogDTO();

            tourLogDTO.Comment = comment;
            tourLogDTO.Difficulty = difficulty;
            tourLogDTO.TotalTime = totalTime;
            tourLogDTO.Rating = rating;
            tourLogDTO.LogId = logId;

            DatabaseConnection.Instance.ExecuteModifyLog(BussinessFactory.Instance.SqlDTO.UpdateLog, tourLogDTO);
        }

        public void Deletelog(string logId)
        {
            DatabaseConnection.Instance.ExecuteDeleteLog(BussinessFactory.Instance.SqlDTO.DeleteLog, logId);
        }

        public List<String> SelectAllRoutes()
        {
            List<String> routeNames = new List<String>();

            try
            {
                DataTable dataTable = DatabaseConnection.Instance.ExecuteSelectAllRoutes(BussinessFactory.Instance.SqlDTO.SelectAllRoutes);
                foreach (DataRow row in dataTable.Rows)
                {
                    try
                    {
                        //Check what is better here: TourName or TourId
                        string routeName = row["TourName"].ToString();
                        routeNames.Add(routeName);
                    }
                    catch (Exception e)
                    {
                        LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                    }
                }
                return routeNames;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return routeNames;
            }
        }

        public void CreateRouteReport(string routeId)
        {
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);

            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

                    httpResponseDTO.Route.Id = row["TourId"].ToString();
                    httpResponseDTO.Route.Name = row["TourName"].ToString();
                    httpResponseDTO.Route.Description = row["TourDescription"].ToString();
                    httpResponseDTO.Route.From = row["TourFrom"].ToString();
                    httpResponseDTO.Route.To = row["TourTo"].ToString();
                    httpResponseDTO.Route.Transport = row["TourTransport"].ToString();
                    httpResponseDTO.Route.Distance = row["TourDistance"].ToString();
                    httpResponseDTO.Route.FormattedTime = row["TourTime"].ToString();
                    httpResponseDTO.Route.ImageUrl = row["TourImage"].ToString();

                    DocumentCreation.Instance.RouteReportCreation(httpResponseDTO);
                }
                catch (Exception e)
                {
                    LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                }
            }
        }
        public void CreateSummarizeReport(string routeId)
        {
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);

            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    string distance = row["TourDistance"].ToString();
                    int avgTime = 0;
                    int avgRating = 0;

                    DataTable dataTableLogs = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
                    int totalRows = dataTableLogs.Rows.Count;

                    foreach(DataRow rowLog in dataTableLogs.Rows)
                    {
                        avgTime += Int32.Parse(row["TotalTime"].ToString());
                        avgRating += Int32.Parse(row["Rating"].ToString());
                    }

                    double finalTime = avgTime / totalRows;
                    double finalRating = avgRating / totalRows;

                    DocumentCreation.Instance.RouteSummarizeReportCreation(finalTime, finalRating, distance);
                }
                catch (Exception e)
                {
                    LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                }
            }
        }

        public void ImportRouteFromFile(string filename)
        {
            FileInfo file = new FileInfo(filename);

            if(file.Extension != "xml")
            {
                LoggerToFile.LogError("ImportFromFile: File is not in right format");
            }
            else
            {
                HttpResponseDTO httpResponseDTO = ImportExport.Instance.ImportFile(filename);
                DatabaseConnection.Instance.ExecuteInsertRoute(BussinessFactory.Instance.SqlDTO.Insert, httpResponseDTO);
            }
        }

        public void ExportRouteToFile(string filename, string routeId)
        {
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);

            foreach(DataRow row in dataTable.Rows)
            {
                HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

                httpResponseDTO.Route.Id = row["TourId"].ToString();
                httpResponseDTO.Route.Name = row["TourName"].ToString();
                httpResponseDTO.Route.Description = row["TourDescription"].ToString();
                httpResponseDTO.Route.From = row["TourFrom"].ToString();
                httpResponseDTO.Route.To = row["TourTo"].ToString();
                httpResponseDTO.Route.Transport = row["TourTransport"].ToString();
                httpResponseDTO.Route.Distance = row["TourDistance"].ToString();
                httpResponseDTO.Route.FormattedTime = row["TourTime"].ToString();
                httpResponseDTO.Route.ImageUrl = row["TourImage"].ToString();

                ImportExport.Instance.ExportFile(filename, httpResponseDTO);
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

            if(totalRows == 0)
            {
                result = "Not popular route";
            }
            else if(totalRows > 0 && totalRows < 5)
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

                    if(distance < 200)
                    {
                        if(finalTime < 120)
                        {
                            if(finalDifficulty < 5)
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
        public static BussinessLogic LogicInstance
        {
            get
            {
                return logicInstance;
            }
        }
    }
}
