using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TourPlanner.Logic
{
    public class GetDistance
    {
        private readonly MapApiStrings _mapApiStrings = new MapApiStrings();

        public double Distance(string source, string destination)
        {
            //https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
            var request = (HttpWebRequest) WebRequest.Create(_mapApiStrings.DistanceUrl(source, destination));
            var response = (HttpWebResponse) request.GetResponse();
            string responseString;

            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
                {
                    responseString = reader.ReadToEnd();
                }
            }
            dynamic json = JObject.Parse(responseString);
            double distance = json.route.distance;
            return distance;
        }
    }
}
