using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Util;
using Microsoft.Win32;
using Newtonsoft.Json;
using TourPlanner.BL.Database.Log;
using TourPlanner.BL.Database.Tour;
using TourPlanner.Model;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL.ImportExport
{
    public class Import
    {
        public void ImportJson()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();
            var fileStream = openFileDialog.OpenFile();

            var streamReader = new StreamReader(fileStream);

            var json = streamReader.ReadToEnd();

            var tourLogic = new TourLogic();
            var logLogic = new LogLogic();
            var bikeLogic = new BikeLogic();
            var carLogic = new CarLogic();



            var tour = JsonConvert.DeserializeObject<AllData>(json);


            if (tour == null) return;

            foreach (var tourData in tour.TourData)
            {
                tourLogic.InsertTours(tourData);
            }

            foreach (var logData in tour.LogData)
            {
                logLogic.InsertLog(logData);
            }

            foreach (var bikeData in tour.BikeData)
            {
                bikeLogic.InsertLog(bikeData);
            }
            
            foreach (var carData in tour.CarData)
            {
                carLogic.InsertLog(carData);
            }
        }
    }
}
