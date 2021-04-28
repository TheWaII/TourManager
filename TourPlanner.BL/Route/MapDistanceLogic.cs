using TourPlanner.DAL.Route;

namespace TourPlanner.BL.Route
{
    public class MapDistanceLogic
    {
        public void SaveImage(string source, string destination)
        {
            var getMap = new GetMap();
            getMap.SaveImage(source, destination);
        }

        public double DistanceInKm(string source, string destination)
        {
            var getDistance = new GetDistance();
            return getDistance.Distance(source, destination) * 1.6;
        }
    }
}