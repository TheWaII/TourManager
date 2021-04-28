using System.Reflection;
using log4net;
using TourPlanner.DAL.Route;

namespace TourPlanner.BL.Route
{
    public class MapDistanceLogic
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void SaveImage(string source, string destination)
        {
            var getMap = new GetMap();
            getMap.SaveImage(source, destination);

            Logger.Info("Route image saved as " + source + "_" + destination + ".jpeg");
        }

        public double DistanceInKm(string source, string destination)
        {
            var getDistance = new GetDistance();
            var distance = getDistance.Distance(source, destination) * 1.6;

            Logger.Info("Got distance from route " + source + " to " + destination + ".");

            return distance;

        }
    }
}