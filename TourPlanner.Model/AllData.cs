using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.Model
{
    [Serializable]
    public class AllData
    {
        public ObservableCollection<TourData> TourData;
        public ObservableCollection<LogData> LogData;
        public ObservableCollection<BikeData> BikeData;
        public ObservableCollection<CarData> CarData;
    }
}
