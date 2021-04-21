﻿using System.Collections.ObjectModel;
using System.Linq;
using TourPlanner.DAL.Tour;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL.Database.Tour
{
    public class TourLogic
    {
        private readonly ITourRepository _iTourRepository = new TourRepository();

        public TourLogic() { }

        public TourLogic(ITourRepository iTourRepository)
        {
            _iTourRepository = iTourRepository;
        }

        public ObservableCollection<TourData> LoadTours()
        {
            ObservableCollection<TourData> tourCollection = new();

            var getTours = _iTourRepository.GetTours();

            foreach (var tours in getTours.ToList().Select(variable => new TourData
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

        public void InsertTours(TourData tourData)
        {
            tourData.TourRoute = tourData.TourSource + "_" + tourData.TourDestination;
            _iTourRepository.Insert(tourData);
        }

        public void DeleteTours(TourData tourData)
        {
            _iTourRepository.Delete(tourData);
        }

        public void UpdateTours(TourData tourData)
        {
            _iTourRepository.Update(tourData);
        }
    }
}