using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels.Tour;

namespace TourPlanner.ViewModels
{
    public class MainViewModel
    {
        public AddTourViewModel AddTourViewModel { get; set; }
        public DockPanelViewModel DockPanelViewModel { get; set; }

        public string LogoImage => @"..\..\img\icons\logo.png";
        public string PlusImage => @"..\..\img\icons\plus.png";
        public string MinusImage => @"..\..\img\icons\minus.png";
        public string OptionsImage => @"..\..\img\icons\hamburger-menu.png";


        public MainViewModel()
        {
            AddTourViewModel = new AddTourViewModel();
            DockPanelViewModel = new DockPanelViewModel();
        }
    }
}
