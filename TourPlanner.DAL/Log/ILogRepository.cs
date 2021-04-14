using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DAL.Log
{
    public interface ILogRepository
    {
        IEnumerable<LogData> GetLogs();

        void Insert(LogData logs);

        void Update(LogData logs);

        void Delete(LogData logs);

    }
}
