using System.Collections.Generic;
using System.IO;
using System.Linq;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL
{
    public class RemoveMaps
    {
        public RemoveMaps(IEnumerable<TourData> tours)
        {
            var exclude = tours.Select(tourData => tourData.TourSource + "_" + tourData.TourDestination + ".jpeg").ToList();
            exclude.Add("error.jpeg");

            var files = Directory.GetFiles(@"..\..\img\route\").Where(s => !exclude.Contains(Path.GetFileName(s)));

            foreach (var item in files)
            {
                File.Delete(item);
            }
        }
    }
}
