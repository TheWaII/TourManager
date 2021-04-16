using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TourPlanner.Model;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log.Car
{
   public class CarRepository : ICarRepository
    {
        public IEnumerable<CarData> GetCars()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            return dbConnection.Query<CarData>("SELECT LogId, MaxSpeed, AvgSpeed, FuelUsed, FuelCost, CaughtSpeeding from CarTour", commandType: CommandType.Text);
        }

        public void Insert(CarData car)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<CarData>("INSERT INTO CarTour(LogId, MaxSpeed, AvgSpeed, FuelUsed, FuelCost, CaughtSpeeding)" +
                                        "VALUES (@LogId, @MaxSpeed, AvgSpeed, @FuelUsed, @FuelCost, @CaughtSpeeding)",
                new { car.LogId, car.MaxSpeed, car.AvgSpeed, car.FuelUsed, car.FuelCost, car.CaughtSpeeding },
                commandType: CommandType.Text);
        }

        public void Update(CarData car)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<CarData>(
                "UPDATE CarTour SET LogId = @LogId, MaxSpeed = @MaxSpeed, AvgSpeed = @AvgSpeed, FuelUsed = @FuelUsed, FuelCost = @FuelCost, CaughtSpeeding = @CaughtSpeeding " +
                "WHERE LogId = @LogId",
                new { car.LogId, car.MaxSpeed, car.AvgSpeed, car.FuelUsed, car.FuelCost, car.CaughtSpeeding },
                commandType: CommandType.Text);
        }

        public void Delete(CarData car)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<CarData>("DELETE FROM CarTour WHERE LogId = @LogId",
                new { car.LogId },
                commandType: CommandType.Text);
        }
    }
}
