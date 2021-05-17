using System.Reflection;
using System.Text;
using log4net;
using TourPlanner.ViewModels.Log;
using TourPlanner.ViewModels.Tour;

namespace TourPlanner.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            DockPanelViewModel = new DockPanelViewModel();

            TourViewModel = new TourViewModel();

            LogViewModel = new LogViewModel();

            BikeLogViewModel = new BikeLogViewModel();

            CarLogViewModel = new CarLogViewModel();

            DarkModeViewModel = new DarkModeViewModel();

        }

        public DockPanelViewModel DockPanelViewModel { get; set; }

        public TourViewModel TourViewModel { get; set; }

        public LogViewModel LogViewModel { get; set; }

        public BikeLogViewModel BikeLogViewModel { get; set; }

        public CarLogViewModel CarLogViewModel { get; set; }

        public DarkModeViewModel DarkModeViewModel { get; set; }

        

    }
}