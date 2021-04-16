using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TourPlanner.BL.Database.Log;
using TourPlanner.Commands;
using TourPlanner.Model;
using TourPlanner.ViewModels.Log;
using TourPlanner.ViewModels.Tour;
using TourPlanner.Model.Log;

namespace TourPlanner.ViewModels
{
    public class MainViewModel
    {
        public DockPanelViewModel DockPanelViewModel { get; set; }

        public TourViewModel TourViewModel { get; set; }

        public LogViewModel LogViewModel { get; set; }

        public BikeLogViewModel BikeLogViewModel { get; set; }

        public CarLogViewModel CarLogViewModel { get; set; }

        public string LogoImage => @"..\..\img\icons\logo.png";

        public string PlusImage => @"..\..\img\icons\plus.png";

        public string MinusImage => @"..\..\img\icons\minus.png";

        public string EditImage => @"..\..\img\icons\edit.png";



        public MainViewModel()
        {
            DockPanelViewModel = new DockPanelViewModel();

            TourViewModel = new TourViewModel();

            LogViewModel = new LogViewModel();

            BikeLogViewModel = new BikeLogViewModel();

            CarLogViewModel = new CarLogViewModel();

        }
    }
}
