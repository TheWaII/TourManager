using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iText.Html2pdf;
using Microsoft.Win32;
using TourPlanner.BL.Database.Log;
using TourPlanner.BL.Database.Tour;
using TourPlanner.Model.Log;

namespace TourPlanner.BL.Reporting
{
    public class Statistic
    {
        public void CreateStatistic(int tourId)
        {
            var tourLogic = new TourLogic();
            //var tourList = tourLogic.LoadTours();

            var logLogic = new LogLogic();
            var logList = logLogic.LoadLogs();

            var bikeLogic = new BikeLogic();
            var bikeList = bikeLogic.LoadBikes();

            var carLogic = new CarLogic();
            var carList = carLogic.LoadCars();


            var file = CreateHtml(logList, bikeList, carList, tourId);

            var saveFileDialog = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf" };

            saveFileDialog.ShowDialog();

            HtmlConverter.ConvertToPdf(
                new FileInfo(file + ".html"),
                new FileInfo(Path.GetFullPath(saveFileDialog.FileName)));

            File.Delete(file);
        }


        private static string CreateHtml(IReadOnlyCollection<LogData> logList, IEnumerable<BikeData> bikeList, IEnumerable<CarData> carList, int tourId)
        {
            const string fileName = "stat";

            var head = new StringBuilder();
            head.Append("<html><head><link rel='stylesheet' type='text/css' href='../../TourPlanner.BL/Reporting/Style.css'></head><body>");
            head.Append("<h1 style='text-align: center;'> Statistic </h1>");
            head.Append("<hr style='width:100%;text-align:left;'>");
            head.Append("<br> <br> <br>");


            
                File.WriteAllText(fileName + ".html", head + General(logList, tourId) +
                                                      Bike(logList, bikeList, tourId) +
                                                      Car(logList, carList, tourId) + "</body>");
            
            
           

            return fileName;
        }


        private static string General(IEnumerable<LogData> logList, int tourId)
        {
            var logToPrint = logList.Where(data => data.TourId == tourId).ToList();

            var parseTime = logToPrint.Select(x => TimeSpan.Parse(x.LogTotalTime));

            
            var totalTime = new TimeSpan(parseTime.Sum(r => r.Duration().Ticks));

            var distanceTraveled = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Sum();

            var longestTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Max();
            var shortestTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Min();
            var avgTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Average();


            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<div class='header'>");
            stringBuilder.Append("<img src='../../img/icons/stack.png' width='30' height='30'/>");
            stringBuilder.Append("<h3> General </h3>");
            stringBuilder.Append("</div>");

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Total time: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} </p> </div>", totalTime);

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Distance traveled: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", distanceTraveled);

            if (logToPrint.Count() == 1) return stringBuilder.ToString();

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Longest tour:  </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", longestTour);

            stringBuilder.Append("<div class='square'> <p style='text-align:center'>Shortest tour: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", shortestTour);

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Average tour:  </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", avgTour);

            return stringBuilder.ToString();
        }

        private static string Bike(IEnumerable<LogData> logList,
            IEnumerable<BikeData> bikeList, int tourId)
        {
            var logToPrint = logList.Where(data => data.TourId == tourId && data.LogType == 1).ToList();
            var bikeData = bikeList.Where(x => logToPrint.Any(y => y.LogId == x.LogId)).ToList();

            if (bikeData.Count == 0) return " ";


            var parseTime = logToPrint.Select(x => TimeSpan.Parse(x.LogTotalTime));

            var totalTime = new TimeSpan(parseTime.Sum(r => r.Duration().Ticks));

            var distanceTraveled = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Sum();
            var longestTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Max();
            var shortestTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Min();
            var avgTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Average();
            var peakHeartRate = bikeData.Select(x => x.PeakHeartRate).DefaultIfEmpty(0).Max();
            var lowestHeartRate = bikeData.Select(x => x.LowestHeartRate).DefaultIfEmpty(0).Min();
            var avgHeartRate = bikeData.Select(x => x.AvgHeartRate).DefaultIfEmpty(0).Average();


            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<div class='header'>");
            stringBuilder.Append("<img src='../../img/icons/bike.png' width='30' height='30'/>");
            stringBuilder.AppendFormat("<h3> Bike ({0}) </h3>", bikeData.Count);
            stringBuilder.Append("</div>");

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Total time: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} </p> </div>", totalTime);

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Distance traveled: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", distanceTraveled);

            if (bikeData.Count != 1)
            {
                stringBuilder.Append("<div class='square'> <p style='text-align:center'> Longest tour:  </p> </div>");
                stringBuilder.Append("<div class='triangleRight'></div>");
                stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", longestTour);

                stringBuilder.Append("<div class='square'> <p style='text-align:center'>Shortest tour: </p> </div>");
                stringBuilder.Append("<div class='triangleRight'></div>");
                stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", shortestTour);

                stringBuilder.Append("<div class='square'> <p style='text-align:center'> Average tour:  </p> </div>");
                stringBuilder.Append("<div class='triangleRight'></div>");
                stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", avgTour);
            }

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Peak Heart rate:  </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} BPM </p> </div>", peakHeartRate);

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Lowest Heart rate: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} BPM </p> </div>", lowestHeartRate);

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Average Heart rate: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} BPM </p> </div>", avgHeartRate);

            return stringBuilder.ToString();
        }

        private static string Car(IEnumerable<LogData> logList, IEnumerable<CarData> carList, int tourId)
        {
            var logToPrint = logList.Where(data => data.TourId == tourId && data.LogType == 2).ToList();
            var carData = carList.Where(x => logToPrint.Any(y => y.LogId == x.LogId)).ToList();

            if (carData.Count == 0) return " ";

            var parseTime = logToPrint.Select(x => TimeSpan.Parse(x.LogTotalTime));

            var totalTime = new TimeSpan(parseTime.Sum(r => r.Duration().Ticks));
            var distanceTraveled = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Sum();
            var longestTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Max();
            var shortestTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Min();
            var avgTour = logToPrint.Select(x => x.LogDistance).DefaultIfEmpty(0).Average();
            var fuelUsed = carData.Select(x => x.FuelUsed).DefaultIfEmpty(0).Max();
            var timesCaughtSpeeding = carData.Count(x => x.CaughtSpeeding);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<div class='header'>");
            stringBuilder.Append("<img src='../../img/icons/car.png' width='30' height='30'/>");
            stringBuilder.AppendFormat("<h3> Car ({0}) </h3>", carData.Count);
            stringBuilder.Append("</div>");

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Total time: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} </p> </div>", totalTime);

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Distance traveled: </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", distanceTraveled);

            if (carData.Count != 1)
            {
                stringBuilder.Append("<div class='square'> <p style='text-align:center'> Longest tour:  </p> </div>");
                stringBuilder.Append("<div class='triangleRight'></div>");
                stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", longestTour);

                stringBuilder.Append("<div class='square'> <p style='text-align:center'>Shortest tour: </p> </div>");
                stringBuilder.Append("<div class='triangleRight'></div>");
                stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", shortestTour);

                stringBuilder.Append("<div class='square'> <p style='text-align:center'> Average tour:  </p> </div>");
                stringBuilder.Append("<div class='triangleRight'></div>");
                stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", avgTour);
            }

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Fuel used:  </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", fuelUsed);

            stringBuilder.Append("<div class='square'> <p style='text-align:center'> Times caught speeding:  </p> </div>");
            stringBuilder.Append("<div class='triangleRight'></div>");
            stringBuilder.AppendFormat("<div class='text'> <p style='text-align:center'> {0} KM </p> </div>", timesCaughtSpeeding);

            return stringBuilder.ToString();
        }
    }
}