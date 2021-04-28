using System.Configuration;

namespace TourPlanner.DAL
{
    public static class Connection
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
    }
}