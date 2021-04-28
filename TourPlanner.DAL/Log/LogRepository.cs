using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log
{
    public class LogRepository : ILogRepository
    {
        public IEnumerable<LogData> GetLogs()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            return dbConnection.Query<LogData>(
                "SELECT LogId, TourId, LogName, LogDate, LogDistance, LogTotalTime, LogRating, LogType, LogReport FROM Logs",
                commandType: CommandType.Text);
        }

        public void Insert(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            dbConnection.Execute(
                "INSERT INTO Logs(TourId, LogName, LogDate, LogDistance, LogTotalTime, LogRating, LogType, LogReport) VALUES (@TourId, @LogName, @LogDate, @LogDistance, @LogTotalTime, @LogRating, @LogType, @LogReport)",
                new
                {
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

        public void Update(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

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

        public void Delete(LogData logs)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (logs == null) return;
            dbConnection.Execute("DELETE FROM logs WHERE LogId = @LogId",
                new {logs.LogId});
        }
    }
}