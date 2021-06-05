using System.Collections.ObjectModel;
using System.Configuration;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using TourPlanner.BL.Database.Log;
using TourPlanner.DAL.Log.Bike;
using TourPlanner.Model.Log;

namespace TourPlanner.UnitTests.Mocking
{
    public class BikeTest
    {
        [Test]
        public void Verify_DatabaseConnectionStringExists()
        {
            Assert.IsNotNull(ConfigurationManager.ConnectionStrings["connection"]);
        }

        [Test]
        public void Load_BikesAreLoaded()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IBikeRepository>()
                .Setup(_ => _.GetBikes())
                .Returns(GetSampleBikes());

            var cls = mock.Create<BikeLogic>();
            var expected = GetSampleBikes();
            var actual = cls.LoadBikes();

            Assert.True(actual != null);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [Test]
        public void Insert_BikesAreInsertedToTable()
        {
            var mock = AutoMock.GetLoose();

            var bike = GetSampleBikes()[0];

            mock.Mock<IBikeRepository>()
                .Setup(_ => _.Insert(bike));

            var cls = mock.Create<BikeLogic>();

            cls.InsertLog(bike);

            mock.Mock<IBikeRepository>()
                .Verify(_ => _.Insert(bike), Times.Once);
        }

        [Test]
        public void Delete_BikesAreDeletedFromTable()
        {
            var mock = AutoMock.GetLoose();

            var bike = GetSampleBikes()[0];

            mock.Mock<IBikeRepository>()
                .Setup(_ => _.Delete(bike.LogId));

            var cls = mock.Create<BikeLogic>();

            cls.DeleteLogs(bike.LogId);

            mock.Mock<IBikeRepository>()
                .Verify(_ => _.Delete(bike.LogId), Times.Once);
        }

        [Test]
        public void Update_BikesAreUpdatedInTable()
        {
            var mock = AutoMock.GetLoose();

            var bike = GetSampleBikes()[0];

            mock.Mock<IBikeRepository>()
                .Setup(_ => _.Delete(bike.LogId));

            var cls = mock.Create<BikeLogic>();

            cls.UpdateLogs(bike);

            mock.Mock<IBikeRepository>()
                .Verify(_ => _.Update(bike), Times.Once);
        }

        private static ObservableCollection<BikeData> GetSampleBikes()
        {
            var output = new ObservableCollection<BikeData>
            {
                new BikeData
                {
                    LogId = 1,
                    PeakHeartRate = 140,
                    LowestHeartRate = 70,
                    AvgHeartRate = 100,
                    AvgSpeed = 20,
                    CaloriesBurnt = 1000
                }
            };
            return output;
        }
    }
}