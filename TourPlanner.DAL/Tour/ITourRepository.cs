using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DAL.Tour
{
    public interface ITourRepository
    {
        IEnumerable<Tours> GetTours();

        void Insert(Tours tours);

        void Update(Tours tours);

        void Delete(Tours tours);

    }
}
