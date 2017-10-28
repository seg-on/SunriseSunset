using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Web.Script.Serialization;
using System.Windows.Forms.VisualStyles;
using System.Globalization;


namespace SunriseSunset
{
    class SST
    {
        public GeoCoordinate locality;

        public void Test()
        {
            locality = new GeoCoordinate();
            //Piestany
            locality.Latitude = 48.584167;
            locality.Longitude = 17.833611;

            string Today = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            //string json = new WebClient().DownloadString("https://api.sunrise-sunset.org/json?lat=48.584167&lng=-17.833611&date=2017-10-25");
            //string json = new WebClient().DownloadString("https://api.sunrise-sunset.org/json?lat=48.584167&lng=17.833611&date=2017-10-25");
            string lat = locality.Latitude.ToString(CultureInfo.InvariantCulture);
            string lon = locality.Longitude.ToString(CultureInfo.InvariantCulture);
            string json = new WebClient().DownloadString("https://api.sunrise-sunset.org/json?lat=" + lat + "&lng="+lon+"&date=" + Today);
            Console.Write(json);
            SSAPI piestanyInfo = new JavaScriptSerializer().Deserialize<SSAPI>(json);

            Console.Write(piestanyInfo.results.PrintPropreties());
            Console.Read();
        }


    }


    public class SSAPI
    {
        public SunInfo results { get; set; }
        public string status { get; set; }
    }
    public class SunInfo
    {
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
        public DateTime solar_noon { get; set; }
        public DateTime day_length { get; set; }
        public DateTime civil_twilight_begin { get; set; }
        public DateTime civil_twilight_end { get; set; }
        public DateTime nautical_twilight_begin { get; set; }
        public DateTime nautical_twilight_end { get; set; }
        public DateTime astronomical_twilight_begin { get; set; }
        public DateTime astronomical_twilight_end { get; set; }

        public string PrintPropreties()
        {
            string allData = System.Environment.NewLine;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
            {
                string name = descriptor.Name;
                DateTime dValue = Convert.ToDateTime(descriptor.GetValue(this));
                string value = dValue.ToLocalTime().ToString();
                allData += name + ":" + value.PadLeft(50- name.Length) +System.Environment.NewLine;
            }
            return allData;
        }

    }
}
