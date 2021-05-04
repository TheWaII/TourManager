using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using iText.Html2pdf;
using TourPlanner.BL.Database.Log;
using TourPlanner.BL.Database.Tour;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL.Reporting
{
    public class Reporting
    {

        public void CreatePdf(int tourId)
        {
            var tourLogic = new TourLogic();
            var tourList = tourLogic.LoadTours();

            var logLogic = new LogLogic();
            var logList = logLogic.LoadLogs();

            var bikeLogic = new BikeLogic();
            var bikeList = bikeLogic.LoadBikes();

            var carLogic = new CarLogic();
            var carList = carLogic.LoadCars();


            var file = TourReport(tourList, logList, bikeList, carList, tourId);

            var path = "../../Report/" + file + "/" + file;

            var htmlFile = path + ".html";
            var pdfFile = path + ".pdf";

            HtmlConverter.ConvertToPdf(
                new FileInfo(htmlFile),
                new FileInfo(pdfFile));

            File.Delete(htmlFile);
        }

        private static string TourReport(IReadOnlyCollection<TourData> tourList, IEnumerable<LogData> logList, IReadOnlyCollection<BikeData> bikeList, IReadOnlyCollection<CarData> carList, int tourId)
        {

            var tourName = tourList.Single(item => item.TourId == tourId).TourName;

            var path = "../../Report/" + tourName + "/" + tourName;
            var reportFile = path + ".html";

            Directory.CreateDirectory("../../Report/" + tourName);

            File.WriteAllText(reportFile, HtmlTourReport(tourList, logList, bikeList, carList, tourId, reportFile, tourName));

            return tourName;
        }

        private static string HtmlTourReport(IReadOnlyCollection<TourData> tourList, IEnumerable<LogData> logList, IReadOnlyCollection<BikeData> bikeList, IReadOnlyCollection<CarData> carList, int tourId, string reportFile, string tourName)
        {
            var stringBuilder = new StringBuilder();

            var source = tourList.Single(item => item.TourId == tourId).TourSource;
            var destination = tourList.Single(item => item.TourId == tourId).TourDestination;
            var distance = tourList.Single(item => item.TourId == tourId).TourDistance;

            var imagePath = "../../img/route/" + source + "_" + destination + ".jpeg";

            //HTML
            stringBuilder.Append("<html><head><link rel='stylesheet' type='text/css' href='../../TourPlanner.BL/Reporting/Style.css'></head><body>");

            //Title
            stringBuilder.Append("<h1 style='text-align: center;'>" + tourName + "</h1>");
            stringBuilder.Append("<hr style='width:100%;text-align:left;'>");
            stringBuilder.Append("<br> <br> <br>");

            //Info
            stringBuilder.Append("<table class='table'>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th width='100px'> <img src='../../img/icons/house.png' width='30' height='30'/>");
            stringBuilder.Append("<th width='100px'> <img src='../../img/icons/destination.png' width='30' height='30'/>");
            stringBuilder.Append("<th width='100px'> <img src='../../img/icons/distance.png' width='30' height='30'/>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='text-align:center'>" + source + "</td>");
            stringBuilder.Append("<td style='text-align:center'>" + destination + "</td>");
            stringBuilder.Append("<td style='text-align:center'>" + distance + " KM </td>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("</table>");

            //Route Image
            stringBuilder.Append("<br><br>");

            stringBuilder.Append("<div style='vertical-align:middle; display:table-cell;text-align:center'>");
            stringBuilder.Append("<img src='" + imagePath + "' style='width:100%; height:100%'>");
            stringBuilder.Append("</div>");

<<<<<<< Updated upstream
            //Log
=======
            //unspecified
>>>>>>> Stashed changes
            var logToPrint = logList.Where(data => data.TourId == tourId).ToList();

            stringBuilder.Append("<br>");

            if (logToPrint.Any(i => i.LogType == 0))
            {
                stringBuilder.Append("<table class='table'> <caption><EM> Unspecified </EM> </caption>");
                stringBuilder.Append("<tr class='tr'>" +
                                     "<th class='th'> Name </th>" +
                                     "<th class='th'>Date</th>" +
                                     "<th class='th'>Distance</th>" +
                                     "<th class='th'>Time</th>" +
                                     "<th class='th'>Rating</th>" +
                                     "<th class='th'>Report</th>" +
                                     "</tr>");
                foreach (var item in logToPrint.Where(item => item.LogType == 0))
                {
                    stringBuilder.AppendFormat("<tr class='tr'><td class='td'>{0}" +
                                               "</td><td class='td'>{1}" +
                                               "</td><td class='td'>{2} KM" +
                                               "</td><td class='td'>{3}" +
                                               "</td><td class='td'>{4}" +
                                               "</td><td class='td'>{5}" +
                                               "</td></tr>",
                        item.LogName, item.LogDate, item.LogDistance, item.LogTotalTime, item.LogRating, item.LogReport);
                }

                stringBuilder.Append("</table>");
            }

            stringBuilder.Append("<br><br><br>");
<<<<<<< Updated upstream


=======
                
            //bike
>>>>>>> Stashed changes
            if (logToPrint.Any(i => i.LogType == 1))
            {
                stringBuilder.Append("<table class='table'><caption><EM> Bike </EM> </caption>");
                stringBuilder.Append("<tr class='tr'>" +
                                     "<th class='th'> Name </th>" +
                                     "<th class='th'>Date</th>" +
                                     "<th class='th'>Distance</th>" +
                                     "<th class='th'>Time</th>" +
                                     "<th class='th'>Rating</th>" +
                                     "<th class='th'>Peak HR</th>" +
                                     "<th class='th'>Lowest HR</th>" +
                                     "<th class='th'>Avg HR</th>" +
                                     "<th class='th'>Avg Speed</th>" +
                                     "<th class='th'>Burnt Cal</th>" +
                                     "<th class='th'>Report</th>" +
                                     "</tr>");
                foreach (var item in logToPrint.Where(item => item.LogType == 1))
                {

                    var peakHeartRate = bikeList.Single(i => i.LogId == item.LogId).PeakHeartRate;
                    var lowestHeartRate = bikeList.Single(i => i.LogId == item.LogId).LowestHeartRate;
                    var avgHeartRate = bikeList.Single(i => i.LogId == item.LogId).AvgHeartRate;
                    var avgSpeed = bikeList.Single(i => i.LogId == item.LogId).AvgSpeed;
                    var calBurnt = bikeList.Single(i => i.LogId == item.LogId).CaloriesBurnt;

                    stringBuilder.AppendFormat("<tr class ='tr'><td class='td'>{0}" +
                                               "</td><td class='td'>{1}" +
                                               "</td><td class='td'>{2} KM" +
                                               "</td><td class='td'>{3}" +
                                               "</td><td class='td'>{4}" +
                                               "</td><td class='td'>{5}" +
                                               "</td><td class='td'>{6}" +
                                               "</td><td class='td'>{7}" +
                                               "</td><td class='td'>{8}" +
                                               "</td><td class='td'>{9}" +
                                               "</td><td class='td'>{10}</td>" +
                                               "</tr>",

                        item.LogName, item.LogDate, item.LogDistance, item.LogTotalTime, item.LogRating,
                        peakHeartRate, lowestHeartRate, avgHeartRate, avgSpeed, calBurnt,
                        item.LogReport);
                }

                stringBuilder.Append("</table>");
            }

            stringBuilder.Append("<br><br><br>");

<<<<<<< Updated upstream

=======
            //car
>>>>>>> Stashed changes
            if (logToPrint.Any(i => i.LogType == 2))
            {
                stringBuilder.Append("<table class='table'><caption><EM> Car </EM> </caption>");
                stringBuilder.Append("<tr class='tr'>" +
                                     "<th class='th'> Name </th>" +
                                     "<th class='th'>Date</th>" +
                                     "<th class='th'>Distance</th>" +
                                     "<th class='th'>Time</th>" +
                                     "<th class='th'>Rating</th>" +
                                     "<th class='th'>Max Speed</th>" +
                                     "<th class='th'>Avg Speed</th>" +
                                     "<th class='th'>Fuel Used</th>" +
                                     "<th class='th'>Fuel Cost</th>" +
                                     "<th class='th'>Caught Speeding</th>" +
                                     "<th class='th'>Report</th>" +
                                     "</tr>");
                foreach (var item in logToPrint.Where(item => item.LogType == 2))
                {

                    var maxSpeed = carList.Single(i => i.LogId == item.LogId).MaxSpeed;
                    var avgSpeed = carList.Single(i => i.LogId == item.LogId).AvgSpeed;
                    var fuelUsed = carList.Single(i => i.LogId == item.LogId).FuelUsed;
                    var fuelCost = carList.Single(i => i.LogId == item.LogId).FuelCost;
                    var caughtSpeeding = carList.Single(i => i.LogId == item.LogId).CaughtSpeeding;

                    stringBuilder.AppendFormat("<tr class ='tr'><td class='td'>{0}" +
                                               "</td><td class='td'>{1}" +
                                               "</td><td class='td'>{2} KM" +
                                               "</td><td class='td'>{3}" +
                                               "</td><td class='td'>{4}" +
                                               "</td><td class='td'>{5}" +
                                               "</td><td class='td'>{6}" +
                                               "</td><td class='td'>{7}" +
                                               "</td><td class='td'>{8}" +
                                               "</td><td class='td'>{9}" +
                                               "</td><td class='td'>{10}</td>" +
                                               "</tr>",

                        item.LogName, item.LogDate, item.LogDistance, item.LogTotalTime, item.LogRating,
                        maxSpeed, avgSpeed, fuelUsed, fuelCost, caughtSpeeding,
                        item.LogReport);
                }

                stringBuilder.Append("</table>");
            }

            stringBuilder.Append("</body></html>");

            var fs = File.Create(reportFile);
            fs.Close();


            return stringBuilder.ToString();
        }
    }
}
