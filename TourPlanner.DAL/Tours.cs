using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL
{
    public class Tours
    {
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourSource { get; set; }
        public string TourDestination { get; set; }
        public double TourDistance { get; set; }
        public string TourDescription { get; set; }
        public string TourRoute { get; set; }



    }
}
