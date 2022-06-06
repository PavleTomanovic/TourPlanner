using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using TourPlanner.DataAccessLayer;
using TourPlanner.Documents;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.BussinesLayer
{
    public class BussinessLogic
    {
        private static BussinessLogic logicInstance = new BussinessLogic();
        ILog log = LogManager.GetLogger(typeof(App));

        public string CreateRoute(string name, string from, string to, string transport, string comment)
        {
            try
            {
                List<TourPreviewDTO> routeNames = SelectTourNameId();

                if (routeNames.Any(n => n.TourName == name)) //routeNames.Contains(name)
                {
                    return "exists";
                }
                else
                {
                    HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;
                    HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

                    httpDTO.From = from;
                    httpDTO.To = to;

                    httpResponseDTO = HttpRequest.Instance.GetRoutes(httpDTO);
                    httpResponseDTO.Route.ImageUrl = HttpRequest.Instance.GetRouteImage(httpDTO);

                    if (httpResponseDTO.Route.Distance == "0")
                    {
                        return "null";
                    }
                    else
                    {
                        httpResponseDTO.Route.Name = name;
                        httpResponseDTO.Route.Comment = comment;
                        httpResponseDTO.Route.From = from;
                        httpResponseDTO.Route.To = to;
                        httpResponseDTO.Route.Transport = transport;

                        DatabaseConnection.Instance.ExecuteInsertRoute(BussinessFactory.Instance.SqlDTO.Insert, httpResponseDTO);

                        return "done";
                    }
                    log.Debug("CreateRoute done");
                }
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("CreateRoute Error" + e.Message + " - " + e.StackTrace);
                return "null";
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

                if (httpResponseDTO.Route.Distance == "0")
                {
                    return false;
                }
                else
                {
                    httpResponseDTO.Route.Name = name;
                    httpResponseDTO.Route.Comment = comment;
                    httpResponseDTO.Route.From = from;
                    httpResponseDTO.Route.To = to;
                    httpResponseDTO.Route.Transport = transport;
                    httpResponseDTO.Route.Id = routeId;

                    DatabaseConnection.Instance.ExecuteUpdateRoute(BussinessFactory.Instance.SqlDTO.Update, httpResponseDTO);
                    return true;
                }
                log.Debug("ModifyRoute done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ModifyRoute Error" + e.Message + " - " + e.StackTrace);
                return false;
            }
        }
        public bool DeleteRoute(string routeId)
        {
            try
            {
                DatabaseConnection.Instance.ExecuteDeleteRoute(BussinessFactory.Instance.SqlDTO.Delete, routeId);
                log.Debug("DeleteRoute done");
                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("DeleteRoute Error" + e.Message + " - " + e.StackTrace);
                return false;
            }
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

                log.Debug("CreateLog done");

                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("CreateLog Error" + e.Message + " - " + e.StackTrace);
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

                log.Debug("ModifyLog done");
                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ModifyLog Error" + e.Message + " - " + e.StackTrace);
                return false;
            }
        }
        public void Deletelog(string routeId, string logId)
        {
            try
            {
                DatabaseConnection.Instance.ExecuteDeleteLog(BussinessFactory.Instance.SqlDTO.DeleteLog, routeId, logId);
                log.Debug("Deletelog done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("Deletelog Error" + e.Message + " - " + e.StackTrace);
            }
        }
        public List<TourPreviewDTO> SelectTourNameId()
        {
            List<TourPreviewDTO> routeNameId = new List<TourPreviewDTO>();

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
                        routeNameId.Add(new TourPreviewDTO { TourName = routeName, TourId = routeId });
                    }
                    catch (Exception e)
                    {
                        LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                    }
                }

                log.Debug("SelectTourNameId done");

                return routeNameId;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("SelectTourNameId Error" + e.Message + " - " + e.StackTrace);
                return routeNameId;
            }
        }
        public bool CreateRouteReport(string routeId)
        {
            try
            {
                HttpResponseDTO httpResponseDTO = SelectAllFromRoute(routeId);
                DocumentCreation.Instance.RouteReportCreation(httpResponseDTO);

                log.Debug("CreateRouteReport done");

                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("CreateRouteReport Error" + e.Message + " - " + e.StackTrace);
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
                    int avgTimeInt = 0;
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
                        avgTimeInt += Convert.ToInt32(avgTimeString);
                        avgRating += Convert.ToDouble(avgRatingString);
                    }
                    int hours = 0;
                    double finalTimeHours = 0;
                    double finalTimeRest = 0;
                    double finalTimeMinutes = 0;
                    double finalRating = 0;

                    if (totalRows > 0)
                    {
                        finalTimeHours = avgTime / totalRows;
                        avgTime = Convert.ToInt32(avgTime);
                        hours = avgTimeInt / totalRows;
                        finalTimeRest = finalTimeHours % totalRows;
                        finalTimeMinutes = finalTimeRest * 60;
                        finalRating = Math.Round(avgRating / totalRows, 2);
                    }

                    string finalTimeString = hours.ToString() + ":" + finalTimeMinutes.ToString();

                    DocumentCreation.Instance.RouteSummarizeReportCreation(finalTimeString, finalRating, distance, routeName);

                    log.Debug("CreateSummarizeReport done");
                }
                catch (Exception e)
                {
                    LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                    log.Error("CreateSummarizeReport Error" + e.Message + " - " + e.StackTrace);
                    return false;
                }
            }

            return true;
        }
        public string ImportRouteFromFile(string filename)
        {
            try
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

                    List<TourPreviewDTO> routeNames = SelectTourNameId();

                    if (routeNames.Any(n => n.TourName == httpResponseDTO.Route.Name))
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
                log.Debug("ImportRouteFromFile done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ImportRouteFromFile Error" + e.Message + " - " + e.StackTrace);
                return "";
            }
        }
        public string calcTime(string time)
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

            try
            {
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

                log.Debug("SelectAllFromRoute done");

                return httpResponseDTO;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("SelectAllFromRoute Error" + e.Message + " - " + e.StackTrace);
                return httpResponseDTO;
            }
        }
        public bool ExportRouteToFile(string filename, string routeId)
        {
            try
            {
                HttpResponseDTO httpResponseDTO = SelectAllFromRoute(routeId);
                ImportExport.Instance.ExportFile(filename, httpResponseDTO);

                log.Debug("ExportRouteToFile done");

                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("ExportRouteToFile Error" + e.Message + " - " + e.StackTrace);
                return false;
            }
        }
        public void MakeRouteFavorite(string routeId)
        {
            try
            {
                DatabaseConnection.Instance.ExecuteFavorite(BussinessFactory.Instance.SqlDTO.UpdateFavorite, routeId);
                log.Debug("MakeRouteFavorite done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("MakeRouteFavorite Error" + e.Message + " - " + e.StackTrace);
            }
        }
        public void DeleteRouteFromFavorites(string routeId)
        {
            try
            {
                DatabaseConnection.Instance.ExecuteFavorite(BussinessFactory.Instance.SqlDTO.UpdateNoFavorite, routeId);
                log.Debug("DeleteRouteFromFavorites done");
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("DeleteRouteFromFavorites Error" + e.Message + " - " + e.StackTrace);
            }
        }
        public string CheckRoutePopularity(string routeId)
        {
            string result;

            DataTable dataTableLogs = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
            int totalRows = dataTableLogs.Rows.Count;
            try
            {
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

                log.Debug("CheckRoutePopularity done");

                return result;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("CheckRoutePopularity Error" + e.Message + " - " + e.StackTrace);
                return "";
            }
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
                    log.Debug("CheckRouteChildFriendliness done");
                }
                catch (Exception e)
                {
                    LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                    log.Error("CheckRouteChildFriendliness Error" + e.Message + " - " + e.StackTrace);
                }
            }
            return result;
        }
        public List<TourPreviewDTO> PrepareListRouteForSearch(string searchText)
        {
            List<TourPreviewDTO> result = new List<TourPreviewDTO>();
            List<string> routeIds = new List<string>();

            try
            {
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
                        result.Add(new TourPreviewDTO { TourName = tourName, TourId = tourId });
                    }
                }

                log.Debug("PrepareListRouteForSearch done");

                return result;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("PrepareListRouteForSearch Error" + e.Message + " - " + e.StackTrace);
                return result;
            }
        }
        public List<TourLogDTO> SelectLogForRoute(string tourId)
        {
            DataTable dataTable = DatabaseConnection.Instance.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, tourId);
            List<TourLogDTO> list = new List<TourLogDTO>();
            try
            {
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

                log.Debug("SelectLogForRoute done");

                return list;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("SelectLogForRoute Error" + e.Message + " - " + e.StackTrace);
                return list;
            }

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
