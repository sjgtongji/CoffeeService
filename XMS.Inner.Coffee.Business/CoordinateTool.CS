using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public class CoordinateTool
    {
        // Methods
        public CoordinateTool(double Longitude, double Latitude)
        {
            this.Longitude = Longitude;
            this.Latitude = Latitude;
        }
        public double Distance(CoordinateTool coordinateTool)
        {
            return GetDistance(this.Latitude, this.Longitude, coordinateTool.Latitude, coordinateTool.Longitude);
        }
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);
            double s = 2.0 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2.0), 2.0) + ((Math.Cos(radLat1) * Math.Cos(radLat2)) * Math.Pow(Math.Sin(b / 2.0), 2.0))));
            s *= 6378.137;
            return (Math.Round((double)(s * 10000.0)) / 10000.0);
        }
        private static double rad(double d)
        {
            return ((d * 3.1415926535897931) / 180.0);
        }

        // Properties
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public object obj { get; set; }
    }
}