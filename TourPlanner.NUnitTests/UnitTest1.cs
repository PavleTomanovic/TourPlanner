namespace TourPlanner.NUnitTests
{
    [TestFixture]
    public class Tests
    {
        BussinessLogic DB = BussinessLogic.LogicInstance;

        public string TourId { get; set; }
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
        public void SelectAllFromRoute_Test()
        {
            HttpResponseDTO response = DB.SelectAllFromRoute(TourId);
            Assert.IsNotEmpty(response.Route.To);
        }
        [Test, Order(5)]
        public void calcTime_Test()
        {
            string response = DB.calcTime("13489");      //3:44 h
            Assert.AreEqual(response, "3:44");
        }
        // [Test, Order(5)]
        public void DeleteRoute_Test()
        {
            bool result = DB.DeleteRoute(TourId);
            Assert.IsTrue(result);
        }

    }
}