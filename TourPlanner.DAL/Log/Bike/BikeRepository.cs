using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using Npgsql;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log.Bike
{
    public class BikeRepository : IBikeRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<BikeData> GetBikes()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            IEnumerable<BikeData> list = null;

            try
            {
                list = dbConnection.Query<BikeData>(
                    "SELECT LogId, PeakHeartRate, LowestHeartRate, AvgHeartRate, AvgSpeed, CaloriesBurnt from BikeTour",
                    commandType: CommandType.Text);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return list;
        }

        public void Insert(BikeData bike)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (bike == null)
            {
                Logger.Error("BikeData on Insert returned NULL");
                return;
            }

            try
            {
                dbConnection.Execute(
                    "INSERT INTO BikeTour(LogId, PeakHeartRate, LowestHeartRate, AvgHeartRate, AvgSpeed, CaloriesBurnt)" +
                    "VALUES (@LogId, @PeakHeartRate, @LowestHeartRate, @AvgHeartRate, @AvgSpeed, @CaloriesBurnt)",
                    new
                    {
                        bike.LogId,
                        bike.PeakHeartRate,
                        bike.LowestHeartRate,
                        bike.AvgHeartRate,
                        bike.AvgSpeed,
                        bike.CaloriesBurnt
                    });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        public void Update(BikeData bike)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (bike == null)
            {
                Logger.Error("BikeData on Update return NULL");
                return;
            }

            try
            {
                dbConnection.Execute(
                    "UPDATE BikeTour SET LogId = @LogId, PeakHeartRate = @PeakHeartRate, LowestHeartRate = @LowestHeartRate, AvgHeartRate = @AvgHeartRate, AvgSpeed = @AvgSpeed, CaloriesBurnt = @CaloriesBurnt WHERE LogId = @LogId",
                    new
                    {
                        bike.LogId,
                        bike.PeakHeartRate,
                        bike.LowestHeartRate,
                        bike.AvgHeartRate,
                        bike.AvgSpeed,
                        bike.CaloriesBurnt
                    });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        public void Delete(int logId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            try
            {
                dbConnection.Execute("DELETE FROM BikeTour WHERE LogId = @logId");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}