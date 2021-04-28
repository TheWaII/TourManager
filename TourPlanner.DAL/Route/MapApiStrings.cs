using System.Configuration;

namespace TourPlanner.DAL.Route
{
    public class MapApiStrings
    {
        private static string Key => ConfigurationManager.ConnectionStrings["key"].ConnectionString;

        internal string DistanceUrl(string source, string destination)
        {
            return "https://www.mapquestapi.com/directions/v2/route?key=" + Key + "&from=" + source + "&to" + "=" +
                   destination +
                   "&outFormat=json&ambiguities=ignore&routeType=fastest&doReverseGeocode=false&enhancedNarrative=false&avoidTimedConditions=false\r\n";
        }

        public string MapUrl(string source, string destination)
        {
            return "https://www.mapquestapi.com/staticmap/v5/map?start=" +
                   source + "&" + "end=" +
                   destination + "&size=600,400@2x&key=" + Key;
        }
    }
}