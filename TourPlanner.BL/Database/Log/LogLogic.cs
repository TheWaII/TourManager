using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TourPlanner.DAL.Log;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL.Database.Log
{
    public class LogLogic
    {
        private readonly ILogRepository _iLogRepository = new LogRepository();

        public LogLogic()
        {
        }

        public LogLogic(ILogRepository iLogRepository)
        {
            _iLogRepository = iLogRepository;
        }

        public void InsertLog(LogData logData)
        {
            _iLogRepository.Insert(logData);
        }

        public void DeleteLogs(LogData logData)
        {
            _iLogRepository.Delete(logData);
        }

        public void UpdateLogs(LogData logData)
        {
            _iLogRepository.Update(logData);
        }

        public ObservableCollection<LogData> LoadLogs()
        {
            ObservableCollection<LogData> logCollection = new();

            var getLogs = _iLogRepository.GetLogs();

            foreach (var logs in getLogs.ToList().Select(variable => new LogData
            {
                LogId = variable.LogId,
                TourId = variable.TourId,
                LogName = variable.LogName,
                LogDate = variable.LogDate,
                LogDistance = variable.LogDistance,
                LogTotalTime = variable.LogTotalTime,
                LogRating = variable.LogRating,
                LogType = variable.LogType,
                LogReport = variable.LogReport
            }))
                logCollection.Add(logs);

            var orderedCollection = logCollection.OrderBy(_ => _.TourId).ThenBy(_ => _.LogName);

            logCollection = new ObservableCollection<LogData>(orderedCollection);

            return logCollection;
        }


        public IEnumerable<LogData> MyFilteredItems(string searchText, IEnumerable<LogData> logCollection)
        {
            return searchText == null
                ? logCollection
                : logCollection.Where(x => x.LogName.Contains(searchText));
        }
    }
}