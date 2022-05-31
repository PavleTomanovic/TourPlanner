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
            /*
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);
            
            foreach(DataRow row in dataTable.Rows)
            {
                string imagePath = row["TourImage"].ToString();

                File.Delete(imagePath);
            }
            */

            DatabaseConnection.Instance.ExecuteDeleteRoute(BussinessFactory.Instance.SqlDTO.Delete, routeId);
        }
        public bool CreateLog(string comment, string difficulty, string totalTime, string rating, string routeId, string dateTime)
        {
            try
            {
                TourLogDTO tourLogDTO = new TourLogDTO();

                tourLogDTO.DateTime = dateTime;
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
        public bool ModifyLog(string comment, string difficulty, string totalTime, string rating, string logId, string routeId, string dateTime)
        {
            try
            {
                TourLogDTO tourLogDTO = new TourLogDTO();
                tourLogDTO.Comment = comment;
                tourLogDTO.Difficulty = difficulty;
                tourLogDTO.TotalTime = totalTime;
                tourLogDTO.Rating = rating;
                tourLogDTO.LogId = logId;
                tourLogDTO.RouteId = routeId;
                tourLogDTO.DateTime = dateTime;
                DatabaseConnection.Instance.ExecuteModifyLog(BussinessFactory.Instance.SqlDTO.UpdateLog, tourLogDTO);
                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        public void Deletelog(string routeId, string logId)
        {
            DatabaseConnection.Instance.ExecuteDeleteLog(BussinessFactory.Instance.SqlDTO.DeleteLog, routeId, logId);
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
                    double avgTime = 0;
                    double avgRating = 0;

                    DataTable dataTableLogs = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
                    int totalRows = dataTableLogs.Rows.Count;

                    foreach (DataRow rowLog in dataTableLogs.Rows)
                    {
                        string avgTimeString = rowLog["TotalTime"].ToString();
                        string avgRatingString = rowLog["Rating"].ToString();
                        avgTimeString = avgTimeString?.Replace(",", ".");
                        avgRatingString = avgRatingString?.Replace(",", ".");
                        avgTime += Convert.ToDouble(avgTimeString);
                        avgRating += Convert.ToDouble(avgRatingString);
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
        private string calcTime(string time)
        {
            int intTime = Convert.ToInt32(time);
            int hours = intTime / 3600;
            int seconds = intTime % 3600;
            int minutes = seconds / 60;
            time = hours.ToString() + ":" + minutes.ToString();
            return time;
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
                httpResponseDTO.Route.Time = calcTime(row["TourTime"].ToString());
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
        public void DeleteRouteFromFavorites(string routeId)
        {
            DatabaseConnection.Instance.ExecuteFavorite(BussinessFactory.Instance.SqlDTO.UpdateNoFavorite, routeId);
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
                    string distanceString = row["TourDistance"].ToString();
                    distanceString = distanceString?.Replace(".", ",");
                    double distance = Convert.ToDouble(distanceString);
                    double avgTime = 0;
                    double avgDifficulty = 0;
                    double finalTime = 0;
                    double finalDifficulty = 0;

                    DataTable dataTableLogs = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
                    double totalRows = dataTableLogs.Rows.Count;

                    foreach (DataRow rowLog in dataTableLogs.Rows)
                    {
                        string avgTimeString = rowLog["TotalTime"].ToString();
                        string avgDifficultyString = rowLog["Difficulty"].ToString();
                        avgTimeString = avgTimeString?.Replace(".", ",");
                        avgDifficultyString = avgDifficultyString?.Replace(".", ",");
                        avgTime += Convert.ToDouble(avgTimeString);
                        avgDifficulty += Convert.ToDouble(avgDifficultyString);
                    }
                    if (totalRows > 0)
                    {
                        finalTime = avgTime / totalRows;
                        finalDifficulty = avgDifficulty / totalRows;
                    }

                    if (distance < 300)
                    {
                        if (finalTime < 3)
                        {
                            if (finalDifficulty < 4)
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
        public List<TourPreview> PrepareListRouteForSearch(string searchText)
        {
            List<TourPreview> result = new List<TourPreview>();
            List<string> routeIds = new List<string>();
            searchText = '%' + searchText + '%';
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SearchThroughRoutes, searchText);
            foreach (DataRow row in dataTable.Rows)
            {
                string tourId = row["TourId"].ToString();
                routeIds.Add(tourId);
            }
            DataTable secondDataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SearchThroughLogs, searchText);
            foreach (DataRow row in secondDataTable.Rows)
            {
                string routeId = row["RouteId"].ToString();
                DataTable thirdDataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);
                foreach (DataRow rowTwo in thirdDataTable.Rows)
                {
                    string tourId = rowTwo["TourId"].ToString();
                    if (!routeIds.Contains(tourId))
                        routeIds.Add(tourId);
                }
            }

            foreach (string routeId in routeIds)
            {
                DataTable thirdDataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);
                foreach (DataRow rowTwo in thirdDataTable.Rows)
                {
                    string tourId = rowTwo["TourId"].ToString();
                    string tourName = rowTwo["TourName"].ToString();
                    result.Add(new TourPreview { tourName = tourName, tourId = tourId });
                }
            }

            return result;
        }
        public List<TourLogDTO> SelectLogForRoute(string tourId)
        {
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, tourId);
            List<TourLogDTO> list = new List<TourLogDTO>();

            foreach (DataRow row in dataTable.Rows)
            {
                string routeId = row["RouteId"].ToString();
                string logId = row["LogId"].ToString();
                string dateTime = row["DateTime"].ToString();
                string difficulty = row["Difficulty"].ToString();
                string comment = row["Comment"].ToString();
                string rating = row["Rating"].ToString();
                string totalTime = row["TotalTime"].ToString();

                list.Add(new TourLogDTO { RouteId = routeId, LogId = logId, DateTime = dateTime, Difficulty = difficulty, Comment = comment, Rating = rating, TotalTime = totalTime });
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
