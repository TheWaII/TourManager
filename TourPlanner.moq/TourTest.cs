using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Configuration;
using TourPlanner.BL.Database;
using TourPlanner.DAL;
using TourPlanner.DAL.Tour;
using TourPlanner.Model;

namespace TourPlanner.moq
{
    public class TourTest
    {

        [Test]
        public void Verify_DatabaseConnectionStringExists()
        {
            Assert.IsNotNull(ConfigurationManager.ConnectionStrings["connection"]);
        }

        [Test]
        public void Load_ToursAreLoaded()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<ITourRepository>()
                .Setup(_ => _.GetTours())
                .Returns(GetSampleTours());

            var cls = mock.Create<DatabaseLogic>();
            var expected = GetSampleTours();
            var actual = cls.LoadTours();

            Assert.True(actual != null);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [Test]
        public void Insert_ToursAreInsertedToTable()
        {
            var mock = AutoMock.GetLoose();

            var tour = GetSampleTours()[0];

            mock.Mock<ITourRepository>()
                .Setup(_ => _.Insert(tour));

            var cls = mock.Create<DatabaseLogic>();

            cls.InsertTours(tour);

            mock.Mock<ITourRepository>()
                .Verify(_ => _.Insert(tour), Times.Once);

        }

        [Test]
        public void Delete_ToursAreDeletedFromTable()
        {
            var mock = AutoMock.GetLoose();

            var tour = GetSampleTours()[0];

            mock.Mock<ITourRepository>()
                .Setup(_ => _.Delete(tour));

            var cls = mock.Create<DatabaseLogic>();

            cls.DeleteTours(tour);

            mock.Mock<ITourRepository>()
                .Verify(_ => _.Delete(tour), Times.Once);
        }

        [Test]
        public void Update_ToursAreUpdatedInTable()
        {
            var mock = AutoMock.GetLoose();

            var tour = GetSampleTours()[0];

            mock.Mock<ITourRepository>()
                .Setup(_ => _.Delete(tour));

            var cls = mock.Create<DatabaseLogic>();

            cls.DeleteTours(tour);

            mock.Mock<ITourRepository>()
                .Verify(_ => _.Delete(tour), Times.Once);
        }

        private static ObservableCollection<Tours> GetSampleTours()
        {
            var output = new ObservableCollection<Tours>
            {
                new Tours
                {
                    TourName = "testTour",
                    TourSource = "testSource",
                    TourDestination = "testDestination",
                    TourDistance = 123.45,
                    TourDescription = "testDescription",
                    TourRoute = "testSourceDestination",
                }
            };
            return output;
        }
    }
}
