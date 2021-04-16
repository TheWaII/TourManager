using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model.Log
{
    public class CarData
    {
        public int LogId { get; set; }

        public int MaxSpeed { get; set; }

        public int AvgSpeed { get; set; }

        public int FuelUsed { get; set; }

        public int FuelCost { get; set; }

        public bool CaughtSpeeding { get; set; }
    }
}
