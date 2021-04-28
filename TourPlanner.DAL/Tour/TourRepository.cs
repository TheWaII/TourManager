using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;
using TourPlanner.Model.Tour;

namespace TourPlanner.DAL.Tour
{
    public class TourRepository : ITourRepository
    {
        public IEnumerable<TourData> GetTours()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            return dbConnection.Query<TourData>(
                "SELECT TourId, TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute from Tours");
        }

        public void Insert(TourData tourData)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (tourData == null) return;

            dbConnection.Execute(
                "INSERT INTO Tours(TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute) VALUES(@TourName, @TourSource, @TourDestination, @TourDistance, @TourDescription, @TourRoute)",
                new
                {
                    tourData.TourName, tourData.TourSource, tourData.TourDestination, tourData.TourDistance,
                    tourData.TourDescription, tourData.TourRoute
                });
        }

        public void Update(TourData tourData)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (tourData == null) return;

            dbConnection.Execute("UPDATE Tours " +
                                 "SET TourName = @TourName, TourSource = @TourSource, TourDestination = @TourDestination, TourDistance = @TourDistance, TourDescription = @TourDescription, TourRoute = @TourRoute" +
                                 " WHERE TourId = @TourId",
                new
                {
                    tourData.TourId, tourData.TourName, tourData.TourSource, tourData.TourDestination,
                    tourData.TourDistance, tourData.TourDescription, tourData.TourRoute
                });
        }

        public void Delete(TourData tourData)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);

            if (tourData == null) return;

            dbConnection.Execute("DELETE FROM BikeTour WHERE LogId IN (select LogId from Logs WHERE TourId = @TourId)",
                new {tourData.TourId});

            dbConnection.Execute("DELETE FROM CarTour WHERE LogId IN (SELECT LogId from Logs WHERE TourId = @TourId)",
                new {tourData.TourId});

            dbConnection.Execute("DELETE FROM tours WHERE TourId = @TourId",
                new {tourData.TourId});

            dbConnection.Execute("DELETE FROM LOGS WHERE TourId = @TourId",
                new {tourData.TourId});
        }
    }
}