namespace TourPlanner.NUnitTests
{
    [TestFixture]
    public class Tests
    {
        BussinessLogic DB = BussinessLogic.LogicInstance;
        public string TourId { get; set; }
        public string LogId { get; set; }
        [SetUp]
        public void Setup()
        {
        }
        [Test, Order(1)]
        public void CreateTour_Test()
        {
            var result = DB.CreateRoute("Tour Name", "Wien", "Graz", "Vacation", "Comment");
            Assert.AreEqual(result, "done");
        }
        [Test, Order(2)]
        public void SelectTourNameId_Test()
        {
            List<TourPreviewDTO> list = DB.SelectTourNameId();
            TourPreviewDTO tp = list.Find(x => x.TourName == "Tour Name");
            TourId = tp.TourId;
            Assert.AreEqual("Tour Name", tp.TourName);
        }
        [Test, Order(3)]
        public void ModifyRoute_Test()
        {
            bool result = DB.ModifyRoute("Wien", "Graz", "Tour Name", "Vacation", "New Comment", TourId);
            Assert.IsTrue(result);
        }
        [Test, Order(4)]
        public void calcTime_Test()
        {
            string response = DB.calcTime("13489");      //3:44 h
            Assert.AreEqual(response, "3:44");
        }
        [Test, Order(5)]
        public void SelectAllFromRoute_Test()
        {
            HttpResponseDTO response = DB.SelectAllFromRoute(TourId);
            Assert.IsNotEmpty(response.Route.To);
        }
        [Test, Order(6)]
        public void CreateRouteReport_Test()
        {
            bool response = DB.CreateRouteReport(TourId);
            Assert.IsTrue(response);
        }

        [Test, Order(7)]
        public void CreateSummarizeReport_Test()
        {
            bool response = DB.CreateSummarizeReport(TourId);
            Assert.IsTrue(response);
        }
        [Test, Order(8)]
        public void ExportRouteToFile_Test()
        {
            bool response = DB.ExportRouteToFile(@"C:\Temp\TourPlanner\Export\Test_Export.xml", TourId);
            Assert.IsTrue(response);
        }
        [Test, Order(9)]
        public void CheckRoutePopularity_Test()
        {
            var response = DB.CheckRoutePopularity(TourId);
            Assert.AreEqual(response, "Not popular route");
        }
        [Test, Order(10)]
        public void CheckRouteChildFriendliness_Test()
        {
            var response = DB.CheckRouteChildFriendliness(TourId);
            Assert.AreNotEqual(response, "Child-friendly");
            Assert.AreNotEqual(response, "Not child-friendly");
        }
        [Test, Order(11)]
        public void MakeRouteFavorite_Test()
        {
            bool response = DB.MakeRouteFavorite(TourId);
            Assert.IsTrue(response);
        }
        [Test, Order(12)]
        public void DeleteRouteFromFavorites_Test()
        {
            bool response = DB.DeleteRouteFromFavorites(TourId);
            Assert.IsTrue(response);
        }
        [Test, Order(13)]
        public void CreateLog_Test()
        {
            bool response = DB.CreateLog("Comment", "1", "4", "3", TourId, "05.05.2022 08:00:00");
            Assert.IsTrue(response);
        }
        [Test, Order(14)]
        public void SelectLogForRoute_Test()
        {
            List<TourLogDTO> list = DB.SelectLogForRoute(TourId);
            TourLogDTO tl = list.Last();
            LogId = tl.LogId;
            Assert.IsNotEmpty(LogId);
        }
        [Test, Order(15)]
        public void ModifyLog_Test()
        {
            bool response = DB.ModifyLog("New Comment", "1", "4", "3", LogId, TourId, "05.05.2022 08:00:00");
            Assert.IsTrue(response);
        }
        [Test, Order(16)]
        public void DeleteLog_Test()
        {
            bool response = DB.Deletelog(TourId, LogId);
            Assert.IsTrue(response);
        }
        [Test, Order(17)]
        public void DeleteRoute_Test()
        {
            bool result = DB.DeleteRoute(TourId);
            Assert.IsTrue(result);
        }
    }
}