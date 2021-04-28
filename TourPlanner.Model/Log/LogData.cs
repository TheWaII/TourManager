namespace TourPlanner.Model.Log
{
    public class LogData
    {
        public int LogId { get; set; }

        public int TourId { get; set; }

        public string LogName { get; set; }

        public string LogDate { get; set; }

        public double LogDistance { get; set; }

        public string LogTotalTime { get; set; }

        public int LogRating { get; set; }

        public int LogType { get; set; }

        public string LogReport { get; set; }

        public string BikeCar { get; set; }
    }
}