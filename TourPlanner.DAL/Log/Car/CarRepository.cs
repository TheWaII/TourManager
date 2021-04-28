using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using log4net.Repository.Hierarchy;
using Npgsql;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log.Car
{
    public class CarRepository : ICarRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<CarData> GetCars()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            IEnumerable<CarData> list = null;

            try
            {
                list = dbConnection.Query<CarData>(
                    "SELECT LogId, MaxSpeed, AvgSpeed, FuelUsed, FuelCost, CaughtSpeeding from CarTour",
                    commandType: CommandType.Text);

            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return list;
        }

        public void Insert(CarData car)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (car == null)
            {
                Logger.Error("CarData on Insert returned NULL");
                return;
            }

            try
            {
                dbConnection.Execute(
                    "INSERT INTO CarTour(LogId, MaxSpeed, AvgSpeed, FuelUsed, FuelCost, CaughtSpeeding)" +
                    "VALUES (@LogId, @MaxSpeed, @AvgSpeed, @FuelUsed, @FuelCost, @CaughtSpeeding)",
                    new {car.LogId, car.MaxSpeed, car.AvgSpeed, car.FuelUsed, car.FuelCost, car.CaughtSpeeding});
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
           
        }

        public void Update(CarData car)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (car == null)
            {
                Logger.Error("CarData on Update returned NULL");
                return;
            }

            try
            {
                dbConnection.Execute(
                    "UPDATE CarTour SET LogId = @LogId, MaxSpeed = @MaxSpeed, AvgSpeed = @AvgSpeed, FuelUsed = @FuelUsed, FuelCost = @FuelCost, CaughtSpeeding = @CaughtSpeeding " +
                    "WHERE LogId = @LogId",
                    new {car.LogId, car.MaxSpeed, car.AvgSpeed, car.FuelUsed, car.FuelCost, car.CaughtSpeeding});
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
                dbConnection.Execute("DELETE FROM CarTour WHERE LogId = @logId", commandType: CommandType.Text);

            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}