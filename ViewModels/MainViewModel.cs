using System.Reflection;
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

        }

        public DockPanelViewModel DockPanelViewModel { get; set; }

        public TourViewModel TourViewModel { get; set; }

        public LogViewModel LogViewModel { get; set; }

        public BikeLogViewModel BikeLogViewModel { get; set; }

        public CarLogViewModel CarLogViewModel { get; set; }

        public string LogoImage => @"..\..\img\icons\logo.png";

        public string PlusImage => @"..\..\img\icons\plus.png";

        public string MinusImage => @"..\..\img\icons\minus.png";

        public string EditImage => @"..\..\img\icons\edit.png";

        public string ReloadImage => @"..\..\img\icons\reload.png";

        public string SaveImage => @"..\..\img\icons\save.png";
    }
}