using System;
using System.IO;
using System.Reflection;
using System.Windows;
using log4net;
using Microsoft.Win32;
using Newtonsoft.Json;
using TourPlanner.BL.Database.Log;
using TourPlanner.BL.Database.Tour;
using TourPlanner.Model;

namespace TourPlanner.BL.ImportExport
{
    public class Import
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void ImportJson()
        {
            var openFileDialog = new OpenFileDialog {Filter = "JSON (*.json)|*.json"};

            Stream fileStream;

            openFileDialog.ShowDialog();

            try
            {
                fileStream = openFileDialog.OpenFile();
            }
            catch (Exception e)
            {
                MessageBox.Show("No tours selected", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.Error(e.Message);
                return;
            }

            var streamReader = new StreamReader(fileStream);

            var json = streamReader.ReadToEnd();

            var tourLogic = new TourLogic();
            var logLogic = new LogLogic();
            var bikeLogic = new BikeLogic();
            var carLogic = new CarLogic();


            var tour = JsonConvert.DeserializeObject<AllData>(json);


            if (tour == null) return;

            foreach (var tourData in tour.TourData) tourLogic.InsertTours(tourData);

            foreach (var logData in tour.LogData) logLogic.InsertLog(logData);

            foreach (var bikeData in tour.BikeData) bikeLogic.InsertLog(bikeData);

            foreach (var carData in tour.CarData) carLogic.InsertLog(carData);
        }
    }
}