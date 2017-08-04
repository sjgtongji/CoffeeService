using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Business
{
    public class ObjectComparer
    {
        private bool _IsIgnoreDateTimeProperty { get; set; }
        private Dictionary<string, int> _dicNotCompare = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private bool _IsNeedCheckDic { get; set; }
        public ObjectComparer(bool bIsIgnoreDateTimeProperty, List<string> lstPropertyNotNeedCompare)
        {
            this._IsIgnoreDateTimeProperty = bIsIgnoreDateTimeProperty;
            if (lstPropertyNotNeedCompare != null && lstPropertyNotNeedCompare.Count > 0)
            {
                foreach (string s in lstPropertyNotNeedCompare)
                {
                    _dicNotCompare[s] = 1;
                }
                _IsNeedCheckDic = true;
            }
        }

        public string GetDifference(object objOld, object objNew)
        {
            if (objNew == null)
            {
                throw new ArgumentException("this method can only compare two not null object");
            }
            if (objOld != null )
            { 
                if (objOld.GetType() != objNew.GetType())
                {
                    throw new ArgumentException("this method can only compare two object with same type");
                }
            }

            PropertyInfo[] aProperty = objNew.GetType().GetProperties();
            List<ValuePair> lstRslt = new List<ValuePair>();
            foreach (PropertyInfo p in aProperty)
            {
                //if (_IsIgnoreDateTimeProperty && p.PropertyType == typeof(System.DateTime))
                //    continue;
                if (_IsNeedCheckDic && _dicNotCompare.ContainsKey(p.Name))
                    continue;

                if (_IsIgnoreDateTimeProperty && p.PropertyType == typeof(System.Decimal))
                {
                    decimal? oOldDecimal = null;
                    if (objOld != null)
                    {
                        object oOldObject = p.GetValue(objOld, null);
                        if (oOldObject != null)
                            oOldDecimal = (decimal)oOldObject;
                    }

                    decimal? oNewDecimal = null;
                    if (objNew != null)
                    {
                        object oNewObject = p.GetValue(objNew, null);
                        if (oNewObject != null)
                            oNewDecimal = (decimal)oNewObject;
                    }

                    if (oNewDecimal != oOldDecimal)
                    {
                        ValuePair obj = new ValuePair();
                        obj.K = p.Name;
                        obj.O = oOldDecimal == null ? string.Empty : oOldDecimal.Value.ToString();
                        obj.N = oNewDecimal == null ? string.Empty : oNewDecimal.Value.ToString();
                        lstRslt.Add(obj);
                    }
                    continue;
                }

                object oOld = null;
                if (objOld != null)
                    oOld = p.GetValue(objOld, null);

                object oNew = null;
                if (objNew != null)
                    oNew = p.GetValue(objNew, null);

                string sOld = oOld != null ? oOld.ToString() : String.Empty;
                string sNew = oNew != null ? oNew.ToString() : String.Empty;
                if (sOld != sNew)
                {
                    ValuePair obj = new ValuePair();
                    obj.K = p.Name;
                    obj.O = sOld;
                    obj.N = sNew;
                    lstRslt.Add(obj);
                }

            }
            return XMS.Core.JsonFast.JsonSerializer.Serialize(lstRslt);

        }
    }

    public class ValuePair
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string K { get; set; }

        /// <summary>
        /// 老值
        /// </summary>
        public string O { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string N { get; set; }

    }

    public class ObjectPair
    {
        public object ObjOld { get; set; }
        public object ObjNew { get; set; }
        public string ObjectType { get; set; }
        public string Key { get; set; }
    }
}
