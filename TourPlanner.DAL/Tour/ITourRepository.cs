using System.Collections.Generic;
using TourPlanner.Model.Tour;

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