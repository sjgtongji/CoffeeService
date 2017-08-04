using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMS.Core;

namespace XMS.Inner.Coffee.Business
{
    public static class ExtendClass
    {
        public static string GetRandom(this int length)
        {
            if (length <= 0)
                return string.Empty;

            return StringHelper.GenerateRandom(length, "0123456789");
        }
    }
}