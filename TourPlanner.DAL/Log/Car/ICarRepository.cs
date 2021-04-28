using System.Collections.Generic;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log.Car
{
    public interface ICarRepository
    {
        IEnumerable<CarData> GetCars();

        void Insert(CarData car);

        void Update(CarData car);

        void Delete(int logId);
    }
}