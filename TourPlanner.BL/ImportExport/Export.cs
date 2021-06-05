using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;
using TourPlanner.Model;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL.ImportExport
{
    public class Export
    {
        public void CreateJson(ObservableCollection<TourData> tourData, ObservableCollection<LogData> logData,
            ObservableCollection<BikeData> bikeData, ObservableCollection<CarData> carData)
        {
            var allData = new AllData
            {
                TourData = tourData,
                LogData = logData,
                BikeData = bikeData,
                CarData = carData
            };

            var saveFileDialog = new SaveFileDialog {Filter = "JSON (*.json)|*.json"};

            saveFileDialog.ShowDialog();

            var json = JsonConvert.SerializeObject(allData, Formatting.Indented);


            if (saveFileDialog.FileName != string.Empty) File.WriteAllText(saveFileDialog.FileName, json);
        }
    }
}