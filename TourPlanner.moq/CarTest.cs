using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using TourPlanner.BL.Database.Log;
using TourPlanner.DAL.Log;
using TourPlanner.DAL.Log.Car;
using TourPlanner.Model.Log;

namespace TourPlanner.Moq
{
    public class CarTest
    {
        [Test]
        public void Verify_DatabaseConnectionStringExists()
        {
            Assert.IsNotNull(ConfigurationManager.ConnectionStrings["connection"]);
        }

        [Test]
        public void Load_CarsAreLoaded()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<ICarRepository>()
                .Setup(_ => _.GetCars())
                .Returns(GetSampleCars());

            var cls = mock.Create<CarLogic>();
            var expected = GetSampleCars();
            var actual = cls.LoadCars();

            Assert.True(actual != null);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [Test]
        public void Insert_CarsAreInsertedToTable()
        {
            var mock = AutoMock.GetLoose();

            var car = GetSampleCars()[0];

            mock.Mock<ICarRepository>()
                .Setup(_ => _.Insert(car));

            var cls = mock.Create<CarLogic>();

            cls.InsertLog(car);

            mock.Mock<ICarRepository>()
                .Verify(_ => _.Insert(car), Times.Once);

        }

        [Test]
        public void Delete_CarsAreDeletedFromTable()
        {
            var mock = AutoMock.GetLoose();

            var car = GetSampleCars()[0];

            mock.Mock<ICarRepository>()
                .Setup(_ => _.Delete(car));

            var cls = mock.Create<CarLogic>();

            cls.DeleteLogs(car);

            mock.Mock<ICarRepository>()
                .Verify(_ => _.Delete(car), Times.Once);
        }

        [Test]
        public void Update_CarsAreUpdatedInTable()
        {
            var mock = AutoMock.GetLoose();

            var car = GetSampleCars()[0];

            mock.Mock<ICarRepository>()
                .Setup(_ => _.Delete(car));

            var cls = mock.Create<CarLogic>();

            cls.UpdateLogs(car);

            mock.Mock<ICarRepository>()
                .Verify(_ => _.Update(car), Times.Once);
        }

        private static ObservableCollection<CarData> GetSampleCars()
        {
            var output = new ObservableCollection<CarData>
            {
                new CarData
                {
                    LogId = 1,
                    MaxSpeed = 140,
                    AvgSpeed = 70,
                    FuelUsed = 100,
                    FuelCost = 20,
                    CaughtSpeeding = true
                }
            };
            return output;
        }
    }
}
