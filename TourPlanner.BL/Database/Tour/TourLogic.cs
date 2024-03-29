﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TourPlanner.BL.Database.Log;
using TourPlanner.DAL.Tour;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL.Database.Tour
{
    public class TourLogic
    {
        private readonly ITourRepository _iTourRepository = new TourRepository();

        public TourLogic()
        {
        }

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
                tourCollection.Add(tours);

            var orderedCollection = tourCollection.OrderBy(_ => _.TourId);

            tourCollection = new ObservableCollection<TourData>(orderedCollection);

            return tourCollection;
        }

        public IEnumerable<TourData> MyFilteredItems(string searchText, ObservableCollection<TourData> tourCollection)
        {
            var logs = new LogLogic().LoadLogs();

            var logCollection = logs.Where(logData => logData.LogName.Contains(searchText));

            var toursLogs = tourCollection.Where(x =>
                x.TourId == logCollection.Where(logData => logData.TourId == x.TourId).Select(x => x.TourId)
                    .FirstOrDefault());

            var tours = tourCollection.Where(x => x.TourName.Contains(searchText));

            if (searchText == null)
                return tourCollection;

            var filteredItems = toursLogs as TourData[] ?? toursLogs.ToArray();
            return !filteredItems.Any() ? tours : filteredItems;

            //return searchText == null ? myFilteredItems : myFilteredItems.Where(tourData => tourData.TourName.Contains(searchText));
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