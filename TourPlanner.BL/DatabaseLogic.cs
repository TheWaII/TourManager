using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL;


namespace TourPlanner.BL
{
    public class DatabaseLogic
    {
        public ITourRepository TourRepository;

        public ObservableCollection<Tours> Observable = new();


        public void LoadTours()
        {
            TourRepository = new TourRepository();


            var getTours = TourRepository.GetTours();

            var tours = new Tours();

            foreach (var variable in getTours.ToList())
            {
                tours.TourId = variable.TourId;
                tours.TourName = variable.TourName;
                tours.TourSource = variable.TourSource;
                tours.TourDestination = variable.TourDestination;
                tours.TourDistance = variable.TourDistance;
                tours.TourDescription = variable.TourDescription;
                tours.TourRoute = variable.TourRoute;
                Observable.Add(tours);
                Console.WriteLine(variable.TourName);
            }
        }
    }
}
