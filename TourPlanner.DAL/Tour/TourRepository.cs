using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;
using TourPlanner.Model;

namespace TourPlanner.DAL.Tour
{
    public class TourRepository : ITourRepository
    {
        public IEnumerable<Tours> GetTours()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            return dbConnection.Query<Tours>("SELECT TourId, TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute from Tours", commandType: CommandType.Text);

        }

        public void Insert(Tours tours)
        {

            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<Tours>("INSERT INTO Tours(TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute) VALUES(@TourName, @TourSource, @TourDestination, @TourDistance, @TourDescription, @TourRoute)",
                    new { tours.TourName, tours.TourSource, tours.TourDestination, tours.TourDistance, tours.TourDescription, tours.TourRoute },
                    commandType: CommandType.Text);

        }

        public void Update(Tours tours)
        {

            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<Tours>("UPDATE Tours " +
                                      "SET TourName = @TourName, TourSource = @TourSource, TourDestination = @TourDestination, TourDistance = @TourDistance, TourDescription = @TourDescription, TourRoute = @TourRoute" +
                                      " WHERE TourId = @TourId",
                new {tours.TourId, tours.TourName, tours.TourSource, tours.TourDestination, tours.TourDistance, tours.TourDescription, tours.TourRoute },
                commandType: CommandType.Text);
        }

        public void Delete(Tours tours)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Query<Tours>("DELETE FROM tours WHERE TourId = @TourId",
                new { tours.TourId },
                commandType: CommandType.Text);
        }
    }
}
