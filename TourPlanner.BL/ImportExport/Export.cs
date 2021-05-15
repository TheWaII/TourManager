using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using TourPlanner.Model;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL.ImportExport
{
    public class Export
    {
        public void CreateJson(ObservableCollection<TourData> tourData, ObservableCollection<LogData> logData, ObservableCollection<BikeData> bikeData, ObservableCollection<CarData> carData)
        {

            var allData = new AllData
            {
                TourData = tourData,
                LogData = logData,
                BikeData = bikeData,
                CarData = carData
            };

            var saveFileDialog = new SaveFileDialog { Filter = "JSON (*.json)|*.json" };

            saveFileDialog.ShowDialog();

            var json = JsonConvert.SerializeObject(allData, Formatting.Indented);

            File.WriteAllText(saveFileDialog.FileName, json);

        }
    }
}
