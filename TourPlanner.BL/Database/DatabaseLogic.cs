using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using TourPlanner.DAL;
using TourPlanner.DAL.Tour;
using TourPlanner.Model;

namespace TourPlanner.BL.Database
{
    public class DatabaseLogic
    {
        private readonly ITourRepository _iTourRepository = new TourRepository();

        public DatabaseLogic() { }

        public DatabaseLogic(ITourRepository iTourRepository)
        {
            _iTourRepository = iTourRepository;
        }

        public ObservableCollection<Tours> LoadTours()
        {
            ObservableCollection<Tours> tourCollection = new();

            var getTours = _iTourRepository.GetTours();

            foreach (var tours in getTours.ToList().Select(variable => new Tours
            {
                TourId = variable.TourId,
                TourName = variable.TourName,
                TourSource = variable.TourSource,
                TourDestination = variable.TourDestination,
                TourDistance = variable.TourDistance,
                TourDescription = variable.TourDescription,
                TourRoute = variable.TourRoute
            }))
            {
                tourCollection.Add(tours);
            }

            return tourCollection;
        }

        public void InsertTours(Tours tours)
        {
            tours.TourRoute = tours.TourSource + "_" + tours.TourDestination;
            _iTourRepository.Insert(tours);
        }

        public void DeleteTours(Tours tours)
        {
            _iTourRepository.Delete(tours);
        }

        public void UpdateTours(Tours tours)
        {
            _iTourRepository.Update(tours);
        }
    }
}
