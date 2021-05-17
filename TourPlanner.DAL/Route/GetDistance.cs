using System;
using System.IO;
using System.Net;
using System.Reflection;
using log4net;
using Newtonsoft.Json.Linq;

namespace TourPlanner.DAL.Route
{
    public class GetDistance
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly MapApiStrings _mapApiStrings = new();

        public double Distance(string source, string destination)
        {
            //https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
            var request = (HttpWebRequest)WebRequest.Create(_mapApiStrings.DistanceUrl(source, destination));
            var response = (HttpWebResponse)request.GetResponse();
            string responseString;

            using (var stream = response.GetResponseStream())
            {
                using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
                responseString = reader.ReadToEnd();
            }

            double distance = 0;

            try
            {
                dynamic json = JObject.Parse(responseString);
                distance = json.route.distance;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
            return distance;
        }
    }
}