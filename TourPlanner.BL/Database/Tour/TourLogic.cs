using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Aspose.Pdf.Annotations;
using ComLib.Lang;
using Toolkit.Core.Extensions;
using TourPlanner.BL.Database.Log;
using TourPlanner.DAL.Tour;
using TourPlanner.Model.Log;
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

        public IEnumerable<TourData> MyFilteredItems(string searchText, IEnumerable<TourData> tourCollection)
        {
            var logs = new LogLogic().LoadLogs();

            var logCollection = logs.Where(logData => logData.LogName.Contains(searchText));

            var tourData = tourCollection as TourData[] ?? tourCollection.ToArray();
            var myFilteredItems = tourCollection as TourData[] ?? tourData.ToArray();

            var toursLogs = myFilteredItems.Where(x =>
                x.TourId == (logCollection.Where(logData => logData.TourId == x.TourId)).Select(x=>x.TourId).FirstOrDefault());

            var tours = myFilteredItems.Where(x => x.TourName.Contains(searchText));

            if (searchText == null)
                return tourData;

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