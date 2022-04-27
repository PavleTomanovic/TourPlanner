using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.BussinesLayer
{
    public class BussinessLogic
    {
        private IDatabaseConnection conn;
        private IHttpRequest req;


        public void CreateRoute(string from, string to, string name, string description, string transport)
        {
            HttpDTO httpDTO = new HttpDTO();
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();
            string imageUrl;

            httpResponseDTO.Route.Name = name;
            httpResponseDTO.Route.Description = description;
            httpResponseDTO.Route.From = from;
            httpResponseDTO.Route.To = to;
            httpResponseDTO.Route.Transport = transport;

            httpDTO.From = from;
            httpDTO.To = to;

            httpResponseDTO = req.GetRoutes(httpDTO);
            imageUrl = req.GetRouteImage(httpDTO);

            conn.ExecuteInsertRoute(BussinessFactory.Instance.SqlDTO.Insert, httpResponseDTO, imageUrl);
        }

        public void ModifyRoute(string from, string to, string name, string description, string transport, string routeId)
        {
            HttpDTO httpDTO = new HttpDTO();
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();
            string imageUrl;

            httpResponseDTO.Route.Name = name;
            httpResponseDTO.Route.Description = description;
            httpResponseDTO.Route.From = from;
            httpResponseDTO.Route.To = to;
            httpResponseDTO.Route.Transport = transport;

            httpDTO.From = from;
            httpDTO.To = to;

            httpResponseDTO = req.GetRoutes(httpDTO);
            imageUrl = req.GetRouteImage(httpDTO);

            conn.ExecuteUpdateRoute(BussinessFactory.Instance.SqlDTO.Update, httpResponseDTO, imageUrl, routeId);
        }

        public void DeleteRoute(string routeId)
        {
            conn.ExecuteDeleteRoute(BussinessFactory.Instance.SqlDTO.Delete, routeId);
        }
    }
}
