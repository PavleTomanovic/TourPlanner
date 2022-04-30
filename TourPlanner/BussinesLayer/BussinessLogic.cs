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
        private IDatabaseConnection conn;
        private IHttpRequest req;
        private IDocumentCreation doc;
        private IImportExport impexp;

        public void CreateRoute(string from, string to, string name, string description, string transport)
        {
            HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

            httpResponseDTO.Route.Name = name;
            httpResponseDTO.Route.Description = description;
            httpResponseDTO.Route.From = from;
            httpResponseDTO.Route.To = to;
            httpResponseDTO.Route.Transport = transport;

            httpDTO.From = from;
            httpDTO.To = to;

            httpResponseDTO = req.GetRoutes(httpDTO);
            httpResponseDTO.Route.ImageUrl = req.GetRouteImage(httpDTO);

            conn.ExecuteInsertRoute(BussinessFactory.Instance.SqlDTO.Insert, httpResponseDTO);
        }

        public void ModifyRoute(string from, string to, string name, string description, string transport, string routeId)
        {
            HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

            httpResponseDTO.Route.Name = name;
            httpResponseDTO.Route.Description = description;
            httpResponseDTO.Route.From = from;
            httpResponseDTO.Route.To = to;
            httpResponseDTO.Route.Transport = transport;
            httpResponseDTO.Route.Id = routeId;

            httpDTO.From = from;
            httpDTO.To = to;

            httpResponseDTO = req.GetRoutes(httpDTO);
            httpResponseDTO.Route.ImageUrl = req.GetRouteImage(httpDTO);

            conn.ExecuteUpdateRoute(BussinessFactory.Instance.SqlDTO.Update, httpResponseDTO);
        }

        public void DeleteRoute(string routeId)
        {
            conn.ExecuteDeleteRoute(BussinessFactory.Instance.SqlDTO.Delete, routeId);
        }

        public void CreateLog(string comment, string difficulty, string totalTime, string rating, string routeId)
        {
            TourLogDTO tourLogDTO = new TourLogDTO();

            tourLogDTO.DateTime = DateTime.Now.ToString();
            tourLogDTO.Comment = comment;
            tourLogDTO.Difficulty = difficulty;
            tourLogDTO.TotalTime = totalTime;
            tourLogDTO.Rating = rating;

            conn.ExecuteInsertLog(BussinessFactory.Instance.SqlDTO.InsertLog, tourLogDTO, routeId);
        }

        public void ModifyLog(string comment, string difficulty, string totalTime, string rating, string logId)
        {
            TourLogDTO tourLogDTO = new TourLogDTO();

            tourLogDTO.Comment = comment;
            tourLogDTO.Difficulty = difficulty;
            tourLogDTO.TotalTime = totalTime;
            tourLogDTO.Rating = rating;
            tourLogDTO.LogId = logId;

            conn.ExecuteModifyLog(BussinessFactory.Instance.SqlDTO.UpdateLog, tourLogDTO);
        }

        public void Deletelog(string logId)
        {
            conn.ExecuteDeleteLog(BussinessFactory.Instance.SqlDTO.DeleteLog, logId);
        }

        public void CreateRouteReport(string routeId)
        {
            DataTable dataTable = conn.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);

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

                    doc.RouteReportCreation(httpResponseDTO);
                }
                catch (Exception e)
                {
                    LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                }
            }
        }
        public void CreateSummarizeReport(string routeId)
        {
            DataTable dataTable = conn.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);

            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    string distance = row["TourDistance"].ToString();
                    int avgTime = 0;
                    int avgRating = 0;

                    DataTable dataTableLogs = conn.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectLogReport, routeId);
                    int totalRows = dataTableLogs.Rows.Count;

                    foreach(DataRow rowLog in dataTableLogs.Rows)
                    {
                        avgTime += Int32.Parse(row["TotalTime"].ToString());
                        avgRating += Int32.Parse(row["Rating"].ToString());
                    }

                    double finalTime = avgTime / totalRows;
                    double finalRating = avgRating / totalRows;

                    doc.RouteSummarizeReportCreation(finalTime, finalRating, distance);
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
                HttpResponseDTO httpResponseDTO = impexp.ImportFile(filename);
                conn.ExecuteInsertRoute(BussinessFactory.Instance.SqlDTO.Insert, httpResponseDTO);
            }
        }

        public void ExportRouteToFile(string filename, string routeId)
        {
            DataTable dataTable = conn.ExecuteSelect(BussinessFactory.Instance.SqlDTO.SelectRoute, routeId);

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

                impexp.ExportFile(filename, httpResponseDTO);
            }
        }

        public void MakeRouteFavorite(string routeId)
        {
            conn.ExecuteFavorite(BussinessFactory.Instance.SqlDTO.UpdateFavorite, routeId);
        }
    }
}
