using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using TourPlanner.Model.Log;

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
