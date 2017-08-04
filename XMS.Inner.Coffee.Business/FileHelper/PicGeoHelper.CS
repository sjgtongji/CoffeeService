using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Drawing.Imaging;
using System.Drawing;
using XMS.Core;

namespace XMS.Inner.Coffee.Business
{
    public abstract class PicGeoHelper
    {
        // Methods
        private static float ExifGpsToFloat(PropertyItem propItemRef, PropertyItem propItem)
        {
            uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
            float degrees = ((float)degreesNumerator) / ((float)degreesDenominator);
            uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
            float minutes = ((float)minutesNumerator) / ((float)minutesDenominator);
            uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 0x10);
            uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
            float seconds = ((float)secondsNumerator) / ((float)secondsDenominator);
            float coorditate = (degrees + (minutes / 60f)) + (seconds / 3600f);
            byte[] bytes = new byte[] { propItemRef.Value[0] };
            switch (Encoding.ASCII.GetString(bytes))
            {
                case "S":
                case "W":
                    coorditate = 0f - coorditate;
                    break;
            }
            return coorditate;

        }
        public static FloatPoint GetPoint(Image targetImg)
        {
            FloatPoint obj = new FloatPoint();
            try
            {
                PropertyItem propItemPhotoTime = targetImg.GetPropertyItem(0x132);
                if (propItemPhotoTime != null)
                {
                    string sTime = GetString(propItemPhotoTime.Value);
                    if (!string.IsNullOrWhiteSpace(sTime))
                    {
                        char[] separator = new char[] { ':', ' ' };
                        string[] arrTime = sTime.Split(separator);
                        if ((arrTime != null) && (arrTime.Length >= 6))
                        {
                            obj.PhotoTime = new DateTime(arrTime[0].ConvertToInt32(0), arrTime[1].ConvertToInt32(0), arrTime[2].ConvertToInt32(0), arrTime[3].ConvertToInt32(0), arrTime[4].ConvertToInt32(0), arrTime[5].ConvertToInt32(0));
                        }
                    }
                }
                PropertyItem propItemLatRef = targetImg.GetPropertyItem(1);
                PropertyItem propItemLat = targetImg.GetPropertyItem(2);
                obj.Latitude = ExifGpsToFloat(propItemLatRef, propItemLat);
                PropertyItem propItemLongRef = targetImg.GetPropertyItem(3);
                PropertyItem propItemLong = targetImg.GetPropertyItem(4);
                obj.Longitude = ExifGpsToFloat(propItemLongRef, propItemLong);
                return obj;
            }
            catch (Exception)
            {
                return obj;
            }
        }
        private static string GetString(byte[] b)
        {
            return Encoding.ASCII.GetString(b, 0, b.Length - 1);
        }

        // Nested Types
        public class FloatPoint
        {
            // Properties
            public float Latitude { get; set; }
            public float Longitude { get; set; }
            public DateTime? PhotoTime { get; set; }
        }
    }



}