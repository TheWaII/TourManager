using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;


namespace TourPlanner.DAL
{
    public class TourRepository : ITourRepository
    {
        public IEnumerable<Tours> GetTours()
        {
            using IDbConnection dbConnection = new NpgsqlConnection(Connection.ConnectionString);
            if(dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            return dbConnection.Query<Tours>("select TourId, TourName, TourSource, TourDestination, TourDistance, TourDescription, TourRoute from Tours", commandType: CommandType.Text);

        }

        public bool Insert(Tours tours)
        {
            throw new NotImplementedException();
        }

        public bool Update(Tours tours)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string tourName)
        {
            throw new NotImplementedException();
        }
    }
}
