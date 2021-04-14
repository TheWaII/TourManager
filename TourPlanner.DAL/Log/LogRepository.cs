using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TourPlanner.Model;

namespace TourPlanner.DAL.Log
{
    public class LogRepository : ILogRepository
    {
        public IEnumerable<LogData> GetLogs()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            return dbConnection.Query<LogData>("SELECT LogId, TourId, LogName, LogDate, LogDistance, LogTotalTime, LogRating, LogTourType, LogReport FROM Logs", commandType: CommandType.Text);
        }

        public void Insert(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<LogData>(
                "INSERT INTO Logs(LogId, TourId, LogName, LogDate, LogDistance, LogTotalTime, LogRating, LogTourType, LogReport) VALUES (@LogId, @TourId, @LogName, @LogDate, @LogDistance, @LogTotalTime, @LogRating, @LogTourType, @LogReport)",
                new
                {
                    logs.LogId,
                    logs.TourId,
                    logs.LogName,
                    logs.LogDate,
                    logs.LogDistance,
                    logs.LogTotalTime,
                    logs.LogRating,
                    logs.LogTourType,
                    logs.LogReport
                },
                commandType: CommandType.Text);

        }

        public void Update(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<LogData>(
                "UPDATE Logs" +
                "SET LogId = @LogId, TourId = @TourId, LogName = @LogName, LogDate = @LogDate, LogDistance = @LogDistance, LogTotalTime = @LogTotalTime, LogRating = @LogRating, LogTourType = @LogTourType, " +
                "LogReport =@LogReport) WHERE LogId = @LogId",
                new
                {
                    logs.LogId,
                    logs.TourId,
                    logs.LogName,
                    logs.LogDate,
                    logs.LogDistance,
                    logs.LogTotalTime,
                    logs.LogRating,
                    logs.LogTourType,
                    logs.LogReport
                },
                commandType: CommandType.Text);
        }

        public void Delete(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<LogData>("DELETE FROM tours WHERE LogId = @LogId",
                new { logs.LogId },
                commandType: CommandType.Text);
        }
    }
}
