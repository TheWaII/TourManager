using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace TourPlanner.DAL
{
    public static class Connection
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
    }
}
