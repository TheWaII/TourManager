using System;
using System.Net;

namespace TourPlanner.BL
{
    public class GetMap
    {
        private readonly MapApiStrings _mapApiStrings = new MapApiStrings();

        public void SaveImage(string source, string destination)
        {
            using (var wc = new WebClient())
            {

                var url = _mapApiStrings.MapUrl(source, destination);

                //wc.DownloadFile(new Uri(url), @"../../images/"+source + "_" + destination + ".jpeg");
                wc.DownloadFile(new Uri(url), @"../../img/" +   source + "_" + destination + ".jpeg");
            }
        }
    }
}
