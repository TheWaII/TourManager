namespace TourPlanner.Model.Log
{
    public class CarData
    {
        public int LogId { get; set; }

        public int MaxSpeed { get; set; }

        public int AvgSpeed { get; set; }

        public int FuelUsed { get; set; }

        public decimal FuelCost { get; set; }

        public bool CaughtSpeeding { get; set; }
    }
}