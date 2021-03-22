using System;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.Drawing;

namespace TourPlanner.Logic
{
    internal class GetMap
    {
        private readonly MapApiStrings _mapApiStrings = new MapApiStrings();

        internal void SaveImage(string source, string destination)
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
