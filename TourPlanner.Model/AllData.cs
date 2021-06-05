using System;
using System.Collections.ObjectModel;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.Model
{
    [Serializable]
    public class AllData
    {
        public ObservableCollection<BikeData> BikeData;
        public ObservableCollection<CarData> CarData;
        public ObservableCollection<LogData> LogData;
        public ObservableCollection<TourData> TourData;
    }
}