using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;
using Dapper;
using log4net;
using Npgsql;
using TourPlanner.Model.Tour;

namespace TourPlanner.DAL.Tour
{
    public class TourRepository : ITourRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<TourData> GetTours()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            IEnumerable<TourData> list = null;

            try
            {
                list = dbConnection.Query<TourData>(
                    "SELECT TourId, TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute from Tours");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return list;
        }

        public void Insert(TourData tourData)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (tourData == null)
            {
                Logger.Error("TourData on Insert returned NULL");
                return;
            }

            try
            {
                dbConnection.Execute(
                    "INSERT INTO Tours(TourId, TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute) VALUES(@TourId, @TourName, @TourSource, @TourDestination, @TourDistance, @TourDescription, @TourRoute)",
                    new
                    {
                        tourData.TourId,
                        tourData.TourName,
                        tourData.TourSource,
                        tourData.TourDestination,
                        tourData.TourDistance,
                        tourData.TourDescription,
                        tourData.TourRoute
                    });
            }
            catch (Exception e)
            {
                try
                {
                    dbConnection.Execute(
                        "INSERT INTO Tours(TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute) VALUES(@TourName, @TourSource, @TourDestination, @TourDistance, @TourDescription, @TourRoute)",
                        new
                        {
                            tourData.TourName,
                            tourData.TourSource,
                            tourData.TourDestination,
                            tourData.TourDistance,
                            tourData.TourDescription,
                            tourData.TourRoute
                        });
                }
                catch (Exception exception)
                {
                    Logger.Error(exception.Message);
                }
              
                Logger.Error(e.Message);
            }

        }

        public void Update(TourData tourData)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (tourData == null)
            {
                Logger.Error("TourData on Delete returned NULL");
                return;
            }

            try
            {
                dbConnection.Execute("UPDATE Tours " +
                                     "SET TourName = @TourName, TourSource = @TourSource, TourDestination = @TourDestination, TourDistance = @TourDistance, TourDescription = @TourDescription, TourRoute = @TourRoute" +
                                     " WHERE TourId = @TourId",
                    new
                    {
                        tourData.TourId,
                        tourData.TourName,
                        tourData.TourSource,
                        tourData.TourDestination,
                        tourData.TourDistance,
                        tourData.TourDescription,
                        tourData.TourRoute
                    });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        public void Delete(TourData tourData)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (tourData == null)
            {
                Logger.Error("TourData on Delete returned NULL");
                MessageBox.Show("Please select a Tour to delete.", " ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                dbConnection.Execute("DELETE FROM BikeTour WHERE LogId IN (select LogId from Logs WHERE TourId = @TourId)",
                    new { tourData.TourId });

                dbConnection.Execute("DELETE FROM CarTour WHERE LogId IN (SELECT LogId from Logs WHERE TourId = @TourId)",
                    new { tourData.TourId });

                dbConnection.Execute("DELETE FROM tours WHERE TourId = @TourId",
                    new { tourData.TourId });

                dbConnection.Execute("DELETE FROM LOGS WHERE TourId = @TourId",
                    new { tourData.TourId });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}