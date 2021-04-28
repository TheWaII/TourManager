using System.Collections.Generic;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log.Bike
{
    public interface IBikeRepository
    {
        IEnumerable<BikeData> GetBikes();

        void Insert(BikeData bike);

        void Update(BikeData bike);

        void Delete(int logId);
    }
}