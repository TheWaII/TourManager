using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TourPlanner.Model;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log.Bike
{
    public class BikeRepository : IBikeRepository
    {
        public IEnumerable<BikeData> GetBikes()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            return dbConnection.Query<BikeData>("SELECT LogId, PeakHeartRate, LowestHeartRate, AvgHeartRate, AvgSpeed, CaloriesBurnt from BikeTour", commandType: CommandType.Text);

        }

        public void Insert(BikeData bike)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            dbConnection.Execute("INSERT INTO BikeTour(LogId, PeakHeartRate, LowestHeartRate, AvgHeartRate, AvgSpeed, CaloriesBurnt)" +
                                         "VALUES (@LogId, @PeakHeartRate, @LowestHeartRate, @AvgHeartRate, @AvgSpeed, @CaloriesBurnt)",
                new { bike.LogId, bike.PeakHeartRate, bike.LowestHeartRate, bike.AvgHeartRate, bike.AvgSpeed, bike.CaloriesBurnt });

        }

        public void Update(BikeData bike)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            dbConnection.Execute(
                "UPDATE BikeTour SET LogId = @LogId, PeakHeartRate = @PeakHeartRate, LowestHeartRate = @LowestHeartRate, AvgHeartRate = @AvgHeartRate, AvgSpeed = @AvgSpeed, CaloriesBurnt = @CaloriesBurnt WHERE LogId = @LogId",
            new { bike.LogId, bike.PeakHeartRate, bike.LowestHeartRate, bike.AvgHeartRate, bike.AvgSpeed, bike.CaloriesBurnt });

        }

        public void Delete(int logId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            dbConnection.Execute("DELETE FROM BikeTour WHERE LogId = @logId");
        }
    }
}
