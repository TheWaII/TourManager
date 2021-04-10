using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL
{
    public interface ITourRepository
    {
        IEnumerable<Tours> GetTours();

        bool Insert(Tours tours);

        bool Update(Tours tours);

        bool Delete(string tourName);

    }
}
