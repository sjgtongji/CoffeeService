using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using  XMS.Core;
using System.Collections;
using System.Data;
using Microsoft.International.Converters.PinYinConverter;

namespace XMS.Inner.Coffee.Business
{

    public class HanziStringPinyinManager
    {
        public static readonly HanziStringPinyinManager Instance = new HanziStringPinyinManager();
        private HanziStringPinyinManager() { }

        #region 变量定义
        private const string sCacheNamePrefix = "Cache_HANZIPINYIN";
        #endregion

        #region
        public HanziStringPinyin GetPinyinFromHanziString(string sHanziString, int nNoOfPinYin)
        {
            nNoOfPinYin = Math.Max(1, nNoOfPinYin);
            nNoOfPinYin = Math.Min(32, nNoOfPinYin);
            sHanziString = sHanziString.DoTrim().ToLower();

            if (String.IsNullOrWhiteSpace(sHanziString))
                throw new ArgumentException("Hanzi string is null");
            if (sHanziString.Length > 100)
                return null;


            HanziStringPinyin objRslt = new HanziStringPinyin();
            PinYin[] aPinYin = new PinYin[sHanziString.Length];
            int[] aIndex = new int[aPinYin.Length];

            int nTotalRsltCount = 1;
            for (int i = 0; i < sHanziString.Length; i++)
            {
                string sSingleWord = sHanziString[i].ToString();
                if (!sSingleWord.IsChinese())
                {
                    aPinYin[i] = GetPinyinBySingleNotChineseWord(sSingleWord);
                    aIndex[i] = nTotalRsltCount;
                    continue;
                }
                aPinYin[i] = GetPinYinForCorrespondingHanzi(sSingleWord);
                if (aPinYin[i] == null)
                {
                    aPinYin[i] = GetPinyinBySingleNotChineseWord(sSingleWord);
                }
                else
                {
                    nTotalRsltCount = nTotalRsltCount * aPinYin[i].lstPinyin.Count;
                }
                aIndex[i] = nTotalRsltCount;

            }
            int nRsltCount = Math.Min(nNoOfPinYin, nTotalRsltCount);
            Dictionary<string, int> dicTmpFirstLetter = new Dictionary<string, int>();
            Dictionary<string, int> dicTmpPinyin = new Dictionary<string, int>();
            Dictionary<string, int> dicTmpSPinyin = new Dictionary<string, int>();
            while (objRslt.Pinyin.Count < nRsltCount)
            {
                int nCandidate = objRslt.Pinyin.Count + 1;
                string sPinYinRslt = "";
                string sSPinyinRslt = "";
                string sFirstLetterRslt = "";

                for (int i = 0; i < aPinYin.Length; i++)
                {
                    string sTmp = "";
                    if (aPinYin[i].IsChinese)
                        sTmp = " ";
                    int nCrntIndex = 0;
                    if (i == 0)
                    {
                        nCrntIndex = (nCandidate - 1) % aIndex[i];
                    }
                    else
                    {
                        nCrntIndex = ((nCandidate - 1) / aIndex[i - 1]) % aPinYin[i].lstPinyin.Count;
                    }
                    sPinYinRslt += sTmp + aPinYin[i].lstPinyin[nCrntIndex] + sTmp;
                    sSPinyinRslt += sTmp + aPinYin[i].lstSPinyin[nCrntIndex] + sTmp;
                    sFirstLetterRslt += aPinYin[i].lstPinyin[nCrntIndex].Substring(0, 1).Replace(" ", "");
                }
                sPinYinRslt = sPinYinRslt.FormatMultiSpace(" ").DoTrim();
                sSPinyinRslt = sSPinyinRslt.FormatMultiSpace(" ").DoTrim();
                if (dicTmpPinyin.ContainsKey(sPinYinRslt))
                {
                    Container.LogService.UnexpectedBehavorLogger.Error("duplicate Pinyin happen,Hanzi=" + sHanziString + ",NofoPinyin=" + nNoOfPinYin + ",duplicate pinyin=" + sPinYinRslt);
                }
                else
                {
                    objRslt.Pinyin.Add(sPinYinRslt);
                    dicTmpPinyin[sPinYinRslt] = 1;
                }
                if (dicTmpSPinyin.ContainsKey(sSPinyinRslt))
                {
                    Container.LogService.UnexpectedBehavorLogger.Error("duplicate SPinyin happen,Hanzi=" + sHanziString + ",NofoPinyin=" + nNoOfPinYin + ",duplicate SPinyin=" + sSPinyinRslt);
                }
                else
                {
                    objRslt.SPinyin.Add(sSPinyinRslt);
                    dicTmpPinyin[sSPinyinRslt] = 1;
                }

                if (!dicTmpFirstLetter.ContainsKey(sFirstLetterRslt))
                {
                    objRslt.FirstLetter.Add(sFirstLetterRslt);
                    dicTmpFirstLetter[sFirstLetterRslt] = 1;
                }
            }
            return objRslt;
        }

        private PinYin GetPinyinBySingleNotChineseWord(string sSingleNotChineseWord)
        {
            PinYin objTmp = new PinYin();
            objTmp.lstSPinyin = new List<string>();
            objTmp.lstPinyin.Add(sSingleNotChineseWord);
            objTmp.lstSPinyin = objTmp.lstPinyin;
            objTmp.IsChinese = false;
            return objTmp;
        }

        private PinYin GetPinYinForCorrespondingHanzi(string sHanzi)
        {
            PinYin objPinyin = null;
            try
            {
                if (String.IsNullOrEmpty(sHanzi))
                    return null;
                if (!RegHelper.objInstance.regSingleChinese.IsMatch(sHanzi))
                    return null;
                string sCacheName = sCacheNamePrefix + sHanzi;
                string sQuery = "select  pinyin,spinyin from hanzi_excised where hanzi=@hanzi";
                Hashtable ht = new Hashtable();
                ht["hanzi"] = sHanzi;
                DataRow dr = SqlHelper.MySql.GetSingleDataRow(sQuery, ht);
                string sPinyin = String.Empty;
                string sSPinyin = String.Empty;
                if (dr != null)
                {

                    sPinyin = dr["pinyin"].ToString();
                    sSPinyin = dr["spinyin"].ToString();

                }
                else
                {
                    sPinyin = GetPinYin(sHanzi.ToCharArray()[0]);
                    if (!String.IsNullOrEmpty(sPinyin))
                    {
                        sSPinyin = sPinyin.Replace("ing", "in").Replace("eng", "en");
                        sQuery = "insert hanzi_excised (hanzi,pinyin,spinyin) values(@hanzi,@pinyin,@spinyin)";
                        ht["hanzi"] = sHanzi;
                        ht["Pinyin"] = sPinyin;
                        ht["spinyin"] = sSPinyin;
                        SqlHelper.MySql.Execute(sQuery, ht);


                    }
                }

                if (String.IsNullOrWhiteSpace(sPinyin))
                {
                    Container.LogService.GetLogger("ChineseNotFoundLog").Info("没找到的汉字:" + sHanzi);
                    return null;
                }
                objPinyin = new PinYin();
                string[] sTmp = sPinyin.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] sTmp1 = sSPinyin.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = sTmp.Length;
                Dictionary<string, int> dicTmp = new Dictionary<string, int>();
                for (int i = 0; i < n; i++)
                {
                    objPinyin.lstPinyin.Add(sTmp[i]);
                    objPinyin.lstSPinyin.Add(sTmp1[i]);
                }
                objPinyin.IsChinese = true;

                return objPinyin;
            }
            catch (Exception e)
            {
                Container.LogService.Error("Hanzi=" + sHanzi, e);
            }
            return null;
        }

        private string GetPinYin(char cKey)
        {

            try
            {
                ChineseChar objChar = new ChineseChar(cKey);
                Hashtable ht = new Hashtable();
                string sRslt = "";
                foreach (string s in objChar.Pinyins)
                {
                    if (s == null)
                        continue;
                    string sPy = s.Substring(0, s.Length - 1).ToLower();
                    if (ht[sPy] == null)
                    {
                        ht[sPy] = 1;
                        sRslt += sPy + " ";
                    }
                }
                return sRslt.TrimEnd();
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }



    public class PinYin
    {
        public List<string> lstPinyin;
        public List<string> lstSPinyin;
        public bool IsChinese;

        public PinYin()
        {
            lstPinyin = new List<string>();
            lstSPinyin = new List<string>();

        } 
    }

    [DataContract]
    [Serializable]
    public class HanziStringPinyin
    {
        [DataMember]
        public List<string> Pinyin;
        [DataMember]
        public List<string> SPinyin;
        [DataMember]
        public List<string> FirstLetter;
        public HanziStringPinyin()
        {
            Pinyin = new List<string>();
            SPinyin = new List<string>();
            FirstLetter = new List<string>();
        }
    }
}
