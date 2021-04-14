using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;
using TourPlanner.Model;

namespace TourPlanner.DAL.Tour
{
    public class TourRepository : ITourRepository
    {
        public IEnumerable<TourData> GetTours()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            return dbConnection.Query<TourData>("SELECT TourId, TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute from Tours", commandType: CommandType.Text);

        }

        public void Insert(TourData tourData)
        {

            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<TourData>("INSERT INTO Tours(TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute) VALUES(@TourName, @TourSource, @TourDestination, @TourDistance, @TourDescription, @TourRoute)",
                    new { tourData.TourName, tourData.TourSource, tourData.TourDestination, tourData.TourDistance, tourData.TourDescription, tourData.TourRoute },
                    commandType: CommandType.Text);

        }

        public void Update(TourData tourData)
        {

            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<TourData>("UPDATE Tours " +
                                      "SET TourName = @TourName, TourSource = @TourSource, TourDestination = @TourDestination, TourDistance = @TourDistance, TourDescription = @TourDescription, TourRoute = @TourRoute" +
                                      " WHERE TourId = @TourId",
                new { tourData.TourId, tourData.TourName, tourData.TourSource, tourData.TourDestination, tourData.TourDistance, tourData.TourDescription, tourData.TourRoute },
                commandType: CommandType.Text);
        }

        public void Delete(TourData tourData)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<TourData>("DELETE FROM tours WHERE TourId = @TourId",
                new { tourData.TourId },
                commandType: CommandType.Text);
        }
    }
}
