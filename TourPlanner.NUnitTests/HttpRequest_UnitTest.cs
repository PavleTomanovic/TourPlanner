using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer;

namespace TourPlanner.NUnitTests
{
    [TestFixture]
    public class HttpRequest_UnitTest
    {
        HttpRequest Http = HttpRequest.Instance;

        [SetUp]
        public void Setup()
        {
        }
        [Test, Order(18)]
        public void GetRoutes_Test()
        {
            HttpDTO httpDTO = new HttpDTO();
            httpDTO.From = "Wien";
            httpDTO.To = "Graz";
            httpDTO.Key = "K0JVN5xNvWjQcUkc0Du1DhgaVnx4bGsC";
            httpDTO.Url = @"http://www.mapquestapi.com/directions/v2/route";
            httpDTO.MapUrl = @"https://open.mapquestapi.com/staticmap/v5/map";
            HttpResponseDTO result = Http.GetRoutes(httpDTO);
            Assert.AreEqual(result.Route.Distance, "120.009");
        }
    }
}
