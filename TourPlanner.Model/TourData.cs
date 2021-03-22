using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class TourData
    {
        public string TourName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Distance { get; set; }
        public string Description { get; set; }

        public TourData(string tourName, string source, string destination, double distance, string description)
        {
            this.TourName = tourName;
            this.Source = source;
            this.Destination = destination;
            this.Distance = distance;
            this.Description = description;
        }

        public TourData(string tourName)
        {
            this.TourName = tourName;
        }

        public TourData() { }

    }
}
