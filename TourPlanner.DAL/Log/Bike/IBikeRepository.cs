using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using TourPlanner.Model.Log;

namespace TourPlanner.DAL.Log.Bike
{
    public interface IBikeRepository
    {
        IEnumerable<BikeData> GetBikes();

        void Insert(BikeData bike);

        void Update(BikeData bike);

        void Delete(BikeData bike);
    }
}
