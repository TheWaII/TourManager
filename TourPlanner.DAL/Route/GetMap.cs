using System;
using System.Net;

namespace TourPlanner.DAL.Route
{
    public class GetMap
    {
        private readonly MapApiStrings _mapApiStrings = new();

        public void SaveImage(string source, string destination)
        {
            using var wc = new WebClient();
            var url = _mapApiStrings.MapUrl(source, destination);

            wc.DownloadFileAsync(new Uri(url), @"../../img/route/" + source + "_" + destination + ".jpeg");
        }
    }
}