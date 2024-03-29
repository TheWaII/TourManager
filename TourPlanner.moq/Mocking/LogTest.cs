﻿using System.Collections.ObjectModel;
using System.Configuration;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using TourPlanner.BL.Database.Log;
using TourPlanner.DAL.Log;
using TourPlanner.Model.Log;

namespace TourPlanner.UnitTests.Mocking
{
    public class LogTest
    {
        [Test]
        public void Verify_DatabaseConnectionStringExists()
        {
            Assert.IsNotNull(ConfigurationManager.ConnectionStrings["connection"]);
        }

        [Test]
        public void Load_LogsAreLoaded()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<ILogRepository>()
                .Setup(_ => _.GetLogs())
                .Returns(GetSampleLogs());

            var cls = mock.Create<LogLogic>();
            var expected = GetSampleLogs();
            var actual = cls.LoadLogs();

            Assert.True(actual != null);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [Test]
        public void Insert_LogsAreInsertedToTable()
        {
            var mock = AutoMock.GetLoose();

            var log = GetSampleLogs()[0];

            mock.Mock<ILogRepository>()
                .Setup(_ => _.Insert(log));

            var cls = mock.Create<LogLogic>();

            cls.InsertLog(log);

            mock.Mock<ILogRepository>()
                .Verify(_ => _.Insert(log), Times.Once);
        }

        [Test]
        public void Delete_ToursAreDeletedFromTable()
        {
            var mock = AutoMock.GetLoose();

            var log = GetSampleLogs()[0];

            mock.Mock<ILogRepository>()
                .Setup(_ => _.Delete(log));

            var cls = mock.Create<LogLogic>();

            cls.DeleteLogs(log);

            mock.Mock<ILogRepository>()
                .Verify(_ => _.Delete(log), Times.Once);
        }

        [Test]
        public void Update_ToursAreUpdatedInTable()
        {
            var mock = AutoMock.GetLoose();

            var log = GetSampleLogs()[0];

            mock.Mock<ILogRepository>()
                .Setup(_ => _.Delete(log));

            var cls = mock.Create<LogLogic>();

            cls.UpdateLogs(log);

            mock.Mock<ILogRepository>()
                .Verify(_ => _.Update(log), Times.Once);
        }

        private static ObservableCollection<LogData> GetSampleLogs()
        {
            var output = new ObservableCollection<LogData>
            {
                new LogData
                {
                    LogName = "testLog",
                    LogDate = "01.04.1997",
                    LogDistance = 1234,
                    LogTotalTime = "20:22:33",
                    LogRating = 2,
                    LogType = 1,
                    LogReport = "testLogReport"
                }
            };
            return output;
        }
    }
}