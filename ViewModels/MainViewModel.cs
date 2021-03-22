using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.ViewModels
{
    public class MainViewModel
    {
        public AddTourViewModel AddTourViewModel { get; set; }
        public DockPanelViewModel DockPanelViewModel { get; set; }

        public MainViewModel()
        {
            AddTourViewModel = new AddTourViewModel();
            DockPanelViewModel = new DockPanelViewModel();
        }
    }
}
