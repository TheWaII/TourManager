using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL.Log;
using TourPlanner.DAL.Log.Bike;
using TourPlanner.Model.Log;

namespace TourPlanner.BL.Database.Log
{
    public class BikeLogic
    {
        private readonly IBikeRepository _iBikeRepository = new BikeRepository();

        public BikeLogic() { }

        public BikeLogic(IBikeRepository iBikeRepository)
        {
            _iBikeRepository = iBikeRepository;
        }

        public void InsertLog(BikeData bikeData)
        {
            _iBikeRepository.Insert(bikeData);
        }

        public void DeleteLogs(BikeData bikeData)
        {
            _iBikeRepository.Delete(bikeData);
        }

        public void UpdateLogs(BikeData bikeData)
        {
            _iBikeRepository.Update(bikeData);
        }

        public ObservableCollection<BikeData> LoadBikes()
        {
            ObservableCollection<BikeData> bikeCollection = new();

            var getBike = _iBikeRepository.GetBikes();

            foreach (var bike in getBike.ToList().Select(variable => new BikeData()
            {
                LogId = variable.LogId,
                PeakHeartRate = variable.PeakHeartRate, 
                LowestHeartRate = variable.LowestHeartRate, 
                AvgHeartRate = variable.AvgHeartRate, 
                AvgSpeed = variable.AvgSpeed, 
                CaloriesBurnt = variable.CaloriesBurnt
            }))
            {
                bikeCollection.Add(bike);
            }

            return bikeCollection;
        }
    }
}
