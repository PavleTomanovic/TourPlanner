using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.NUnitTests
{
    [TestFixture]
    public class Document_Test
    {
        DocumentCreation Doc = DocumentCreation.Instance;
        [SetUp]
        public void Setup()
        {

        }
        [Test, Order(19)]
        public void RouteReportCreation_Test()
        {
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();

            httpResponseDTO.Route.Name = "Wien Route";
            httpResponseDTO.Route.Id = "1";
            httpResponseDTO.Route.ImageUrl = @"C:\Temp\TourPlanner\TourImages\testImage.jpg";
            httpResponseDTO.Route.Comment = "Hey";
            httpResponseDTO.Route.From = "Wien";
            httpResponseDTO.Route.To = "Graz";
            httpResponseDTO.Route.Transport = "Car";
            httpResponseDTO.Route.Distance = "120.009";
            httpResponseDTO.Route.Time = "7192";

            bool response = Doc.RouteReportCreation(httpResponseDTO);
            Assert.IsTrue(response);
        }
        [Test, Order(20)]
        public void RouteSummarizeReportCreation_Test()
        {
            bool response = Doc.RouteSummarizeReportCreation("7192", 5 , "120.009", "Wien Route");
            Assert.IsTrue(response);
        }
    }
}
