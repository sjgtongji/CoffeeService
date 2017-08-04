using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMS.Core;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace XMS.Inner.Coffee.Business
{
    public class WordPinYinManager
    {
        public static readonly WordPinYinManager Instance = new WordPinYinManager();
        private WordPinYinManager() { }

        #region 变量定义
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);
        internal const int LOCALE_SYSTEM_DEFAULT = 0x0800;
        internal const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        #endregion

        #region 转化关键字
        /// <summary>
        /// 转化关键字
        /// </summary>
        /// <param name="sIn"></param>
        /// <returns></returns>
        public WordPinYin GetWord_Pinyin(string sIn)
        {

            sIn = FormatSearchKey(sIn);
            if (String.IsNullOrWhiteSpace(sIn))
                return null;

            WordPinYin objRslt = new WordPinYin();
            string sInWithoutSpace = sIn.Replace(" ", "").ToLower();
            objRslt.Word = sIn.Replace(" ", "");
            objRslt.WordWithSpace = sIn;
            if (sInWithoutSpace.IsOnlyEnglishLetterOrDigital())
            {
                if (sIn.Length > 30)
                    return null;
                objRslt.Word = "";
                objRslt.PinYin = sInWithoutSpace;
                objRslt.PinYinWithSpace = sIn;
                objRslt.FirstLetter = sInWithoutSpace;
                objRslt.FirstLetterWithSpace = sIn;
                return objRslt;
            }
            if (sIn.Length > 20)
                return null;
            HanziStringPinyin objPinyin = HanziStringPinyinManager.Instance.GetPinyinFromHanziString(sIn, 1);
            if (objPinyin != null)
            {
                string sPinyin = objPinyin.Pinyin[0].Replace("eng", "en").Replace("ing", "in");
                objRslt.FirstLetter = objPinyin.FirstLetter[0];
                objRslt.PinYin = sPinyin.Replace(" ", "");
                objRslt.PinYinWithSpace = sPinyin;

            }
            return objRslt;

        }

        public string FormatSearchKey(string sKey)
        {
            sKey = sKey.DoTrim();

            //sKey = sKey.Replace("&#39;", "'");
            sKey = sKey.Replace(".", "");
            sKey = ToDBC(sKey);
            sKey = RegHelper.objInstance.regKeyFormat1.Replace(sKey, " ");
            sKey = RegHelper.objInstance.regMultiSpace.Replace(sKey, " ");
            sKey = sKey.DoTrim();
            sKey = ToSimplified(sKey);
            return sKey;
        }

        public static string ToDBC(string input)
        {
            if (input == null) return "";
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }


            return System.Text.RegularExpressions.Regex.Replace(new string(c), @"[^\u001E-\uffff]+", "");
        }

        public string ToSimplified(string sSource)
        {
            String sTarget = new String(' ', sSource.Length);
            int tReturn = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_SIMPLIFIED_CHINESE, sSource, sSource.Length, sTarget, sSource.Length);
            return sTarget;
        }
        #endregion
    }

    public class WordPinYin
    {
        public string Word;
        public string WordWithSpace;
        public string PinYin;
        public string PinYinWithSpace;
        public string FirstLetter;
        public string FirstLetterWithSpace;
    }
}
