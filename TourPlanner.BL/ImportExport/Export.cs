using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using TourPlanner.Model.Log;
using TourPlanner.Model.Tour;

namespace TourPlanner.BL.ImportExport
{
    public class Export
    {


        public void CreateJson(TourData tourData, LogData logData, BikeData bikeData, CarData carData)
        {
            var x= JsonConvert.SerializeObject(new TourLog(tourData, logData, bikeData, carData));

            var sw = new StreamWriter("test.json");
            sw.Write(x);
        }



        //Product product = new Product();
        //product.ExpiryDate = new DateTime(2008, 12, 28);

        //JsonSerializer serializer = new JsonSerializer();
        //serializer.Converters.Add(new JavaScriptDateTimeConverter());
        //serializer.NullValueHandling = NullValueHandling.Ignore;

        //using (StreamWriter sw = new StreamWriter(@"c:\json.txt"))
        //    using (JsonWriter writer = new JsonTextWriter(sw))
        //{
        //    serializer.Serialize(writer, product);
        //    // {"ExpiryDate":new Date(1230375600000),"Price":0}
        //}

    }


    public class TourLog
    {
        public TourLog(TourData tourData, LogData logData, BikeData bikeData, CarData carData)
        {
            TourData = tourData;
            LogData = logData;
            BikeData = bikeData;
            CarData = carData;
        }

        public TourData TourData { get; set; }
        public LogData LogData { get; set; }
        public BikeData BikeData { get; set; }
        public CarData CarData { get; set; }


    }

}
