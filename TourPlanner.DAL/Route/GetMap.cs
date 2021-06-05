using System;
using System.Net;
using System.Reflection;
using log4net;

namespace TourPlanner.DAL.Route
{
    public class GetMap
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly MapApiStrings _mapApiStrings = new();

        public void SaveImage(string source, string destination)
        {
            using var wc = new WebClient();
            var url = _mapApiStrings.MapUrl(source, destination);
            try
            {
                wc.DownloadFileAsync(new Uri(url), @"../../img/route/" + source + "_" + destination + ".jpeg");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}