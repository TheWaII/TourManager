namespace TourPlanner.Model.Tour
{
    public class TourData
    {
        public TourData(int tourId, string tourTourName, string tourSource, string tourDestination, double tourDistance,
            string tourDescription, string tourRoute)
        {
            TourId = tourId;
            TourName = tourTourName;
            TourSource = tourSource;
            TourDestination = tourDestination;
            TourDescription = tourDescription;
            TourDistance = tourDistance;
            TourRoute = tourRoute;
        }

        public TourData(string tourTourName)
        {
            TourName = tourTourName;
        }

        public TourData()
        {
        }

        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourSource { get; set; }
        public string TourDestination { get; set; }
        public double TourDistance { get; set; }
        public string TourDescription { get; set; }
        public string TourRoute { get; set; } = "error";

        public string RouteImage => @"../../img/route/" + TourRoute + ".jpeg";
    }
}