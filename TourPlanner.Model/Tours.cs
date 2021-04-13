using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class Tours
    {
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourSource { get; set; }
        public string TourDestination { get; set; }
        public double TourDistance { get; set; }
        public string TourDescription { get; set; }
        public string TourRoute { get; set; } = "error";

        //public string RouteImage => @"..\..\img\icons\edit.png";

        public string RouteImage => @"../../img/route/" + TourRoute + ".jpeg";

        public Tours(int tourId, string tourTourName, string tourSource, string tourDestination, double tourDistance, string tourDescription, string tourRoute)
        {
            TourId = tourId;
            TourName = tourTourName;
            TourSource = tourSource;
            TourDestination = tourDestination;
            TourDescription = tourDescription;
            TourDistance = tourDistance;
            TourRoute = tourRoute;
        }

        public Tours(string tourTourName)
        {
            TourName = tourTourName;
        }

        public Tours() { }

    }
}
