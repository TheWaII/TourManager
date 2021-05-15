using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using Npgsql;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log
{
    public class LogRepository : ILogRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<LogData> GetLogs()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            IEnumerable<LogData> list = null;

            try
            {
                list = dbConnection.Query<LogData>(
                    "SELECT LogId, TourId, LogName, LogDate, LogDistance, LogTotalTime, LogRating, LogType, LogReport FROM Logs",
                    commandType: CommandType.Text);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return list;
        }

        public void Insert(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (logs == null)
            {
                Logger.Error("LogData on Insert was NULL");
                return;
            }

            try
            {
                dbConnection.Execute(
                    "INSERT INTO Logs(TourId, LogId, LogName, LogDate, LogDistance, LogTotalTime, LogRating, LogType, LogReport) VALUES (@TourId, @LogId, @LogName, @LogDate, @LogDistance, @LogTotalTime, @LogRating, @LogType, @LogReport)",
                    new
                    {
                        logs.LogId,
                        logs.TourId,
                        logs.LogName,
                        logs.LogDate,
                        logs.LogDistance,
                        logs.LogTotalTime,
                        logs.LogRating,
                        logs.LogType,
                        logs.LogReport
                    });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

        }

        public void Update(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (logs == null)
            {
                Logger.Error("LogData on Update was NULL");
                return;
            }

            try
            {
                dbConnection.Execute(
                    "UPDATE Logs SET LogId = @LogId, TourId = @TourId, LogName = @LogName, LogDate = @LogDate, LogDistance = @LogDistance, LogTotalTime = @LogTotalTime, LogRating = @LogRating, LogType = @LogType, LogReport = @LogReport WHERE LogId = @LogId",
                    new
                    {
                        logs.LogId,
                        logs.TourId,
                        logs.LogName,
                        logs.LogDate,
                        logs.LogDistance,
                        logs.LogTotalTime,
                        logs.LogRating,
                        logs.LogType,
                        logs.LogReport
                    });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

        }

        public void Delete(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (logs == null)
            {
                Logger.Error("LogData on Delete was NULL");
                return;
            }

            try
            {
                dbConnection.Execute("DELETE FROM logs WHERE LogId = @LogId",
                    new { logs.LogId });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

        }
    }
}