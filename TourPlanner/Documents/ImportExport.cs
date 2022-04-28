using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BussinesLayer;
using TourPlanner.DataAccessLayer;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.Documents
{
    public class ImportExport : IImportExport
    {
        private IHttpRequest req;
        private XMLReader xmlReader;

        public HttpResponseDTO ImportFile(string filename)
        {
            xmlReader = new XMLReader(filename);
            HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;

            httpDTO.From = xmlReader.getSingleNodeString("Route/From");
            httpDTO.To = xmlReader.getSingleNodeString("Route/To");

            HttpResponseDTO httpResponseDTO = req.GetRoutes(httpDTO);

            httpResponseDTO.Route.ImageUrl = req.GetRouteImage(httpDTO);
            httpResponseDTO.Route.Name = xmlReader.getSingleNodeString("Route/Name");
            httpResponseDTO.Route.Description = xmlReader.getSingleNodeString("Route/Description");
            httpResponseDTO.Route.From = xmlReader.getSingleNodeString("Route/From");
            httpResponseDTO.Route.To = xmlReader.getSingleNodeString("Route/To");
            httpResponseDTO.Route.Transport = xmlReader.getSingleNodeString("Route/Transport");

            return httpResponseDTO;
        }
        public void ExportFile(string filename, HttpResponseDTO httpResponseDTO)
        {
            
        }
    }
}
