using System.Collections.Generic;
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