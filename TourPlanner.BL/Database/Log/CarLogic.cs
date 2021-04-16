using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL.Log.Car;
using TourPlanner.Model.Log;

namespace TourPlanner.BL.Database.Log
{
    public class CarLogic
    {
        private readonly ICarRepository _iCarRepository = new CarRepository();

        public CarLogic() { }

        public CarLogic(ICarRepository iCarRepository)
        {
            _iCarRepository = iCarRepository;
        }

        public void InsertLog(CarData carData)
        {
            _iCarRepository.Insert(carData);
        }

        public void DeleteLogs(CarData carData)
        {
            _iCarRepository.Delete(carData);
        }

        public void UpdateLogs(CarData carData)
        {
            _iCarRepository.Update(carData);
        }

        public ObservableCollection<CarData> LoadCars()
        {
            ObservableCollection<CarData> carCollection = new();

            var getCar = _iCarRepository.GetCars();

            foreach (var car in getCar.ToList().Select(variable => new CarData()
            {
                LogId = variable.LogId,
                MaxSpeed = variable.MaxSpeed,
                AvgSpeed = variable.AvgSpeed,
                FuelUsed = variable.FuelUsed, 
                FuelCost = variable.FuelCost,
                CaughtSpeeding = variable.CaughtSpeeding
            }))
            {
                carCollection.Add(car);
            }

            return carCollection;
        }
    }
}
