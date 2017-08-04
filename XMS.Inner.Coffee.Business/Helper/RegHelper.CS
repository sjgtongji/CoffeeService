using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XMS.Inner.Coffee.Business
{
    public class RegHelper
    {
        public static RegHelper objInstance = new RegHelper();
        public Regex regKeyFormat1 = new Regex(@"[^a-zA-Z0-9\u4E00-\u9FA5\sの]", RegexOptions.Compiled);
        public Regex regMultiSpace = new Regex(@"\s+", RegexOptions.Compiled);

        public Regex regSubject = new Regex(@"\|A(?<subjectId>\d+)A\|.*?\|typeId:(?<typeId>\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public Regex regRegionId = new Regex(@"^[A-Za-z][A-Za-z][A-Za-z_0-9]{2,10}|0$", RegexOptions.Compiled);
        public Regex regStyleId = new Regex(@"^\d|\d?_\d{1,5}|0$", RegexOptions.Compiled);
        public Regex regAvg = new Regex(@"^\d|0$", RegexOptions.Compiled);
        public Regex regOrder = new Regex(@"^[AD]\d{1,2}$", RegexOptions.Compiled);
        public Regex regRegionReplace = new Regex(@"^\s*OR\s*", RegexOptions.Compiled);
        public Regex regAdvertise = new Regex(@"^ResId(\-|\+)((\w{12}\d+(\-|\+)?)+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public Regex regQuery = new Regex(@"^\(\(\s*AND\s*", RegexOptions.Compiled);
       
        public Regex regPicFileFormat1 = new Regex("http://s2.95171.cn/upload/", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public Regex regPicFileFormat2 = new Regex("_small", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public Regex regPicFileFormat3 = new Regex("albumpicimages/", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public Regex regQuery1 = new Regex(@"^\s*AND", RegexOptions.Compiled);
        public Regex regQuery2 = new Regex(@"^\s*OR", RegexOptions.Compiled);
        public Regex regNotDigital = new Regex("[^0-9]", RegexOptions.Compiled);
        public Regex regSingleChinese = new Regex(@"[\u4E00-\u9FA5]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public Regex regSingleWord = new Regex("(&lt;em&gt;)(.{1})(&lt;/em&gt;)", RegexOptions.Compiled);
        public Regex regDigital1 = new Regex("[0-9]+$", RegexOptions.Compiled);
        public Regex regPinyinAndDigital = new Regex(@"^\S+\d+", RegexOptions.Compiled);
        public Regex regMulti1 = new Regex(@",+", RegexOptions.Compiled);
        public Regex regChinesPinyinAndDigital = new Regex(@"([\u4E00-\u9FA5]+|[a-zA-Z' ]+|[0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public Regex regMultiDot = new Regex(@"^\.+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public Regex regNopic = new Regex(@"nopic.(v\d+\.)?jpg$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public Regex regNoDishPic = new Regex(@"dish_nopic.(v\d+\.)?jpg$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
