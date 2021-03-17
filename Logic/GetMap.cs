using System;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.Drawing;

namespace TourPlanner.Logic
{
    internal class GetMap
    {
        internal void SaveImage(string source, string destination)
        {
            using (var wc = new WebClient())
            {

                var url = "https://www.mapquestapi.com/staticmap/v5/map?start=" +
                          source + "&" + "end=" +
                          destination + "&size=600,400@2x&key=omAA9i0xRW5N1d05cWYAVwE7lrzDM3sq";

                //wc.DownloadFile(new Uri(url), @"../../images/"+source + "_" + destination + ".jpeg");
                wc.DownloadFile(new Uri(url), @"../../img/" +   source + "_" + destination + ".jpeg");
            }
        }
    }
}
