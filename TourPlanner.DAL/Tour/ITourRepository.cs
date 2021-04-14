using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DAL.Tour
{
    public interface ITourRepository
    {
        IEnumerable<TourData> GetTours();

        void Insert(TourData tourData);

        void Update(TourData tourData);

        void Delete(TourData tourData);

    }
}
