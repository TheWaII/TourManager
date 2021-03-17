using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;

namespace TourPlanner.Logic
{
    internal class Connection
    {
        private static readonly HttpClient Client = new HttpClient();

        private const string Key = "omAA9i0xRW5N1d05cWYAVwE7lrzDM3sq";



        internal string DistanceUrl(string source, string destination)
        {
            return "https://www.mapquestapi.com/directions/v2/route?key="+ Key + "&from="+ source + "&to" + "="+ destination + 
                   "&outFormat=json&ambiguities=ignore&routeType=fastest&doReverseGeocode=false&enhancedNarrative=false&avoidTimedConditions=false\r\n";
        }

    }
}
