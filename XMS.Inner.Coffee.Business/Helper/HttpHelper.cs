using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Business
{
    //public class HttpHelper
    //{
    //    // Methods
    //    public static string RequestURL(string url, string postData)
    //    {
    //        if (string.IsNullOrEmpty(postData))
    //        {
    //            return RequestURLGet(url);
    //        }
    //        return RequestURLPost(url, postData);

    //    }
    //    private static string RequestURLGet(string url)
    //    {
    //        string responseData = "";
    //        HttpWebResponse response = null;
    //        try
    //        {
    //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    //            request.Method = "GET";
    //            request.Timeout = 0x7530;
    //            response = (HttpWebResponse)request.GetResponse();
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
    //                {
    //                    responseData = reader.ReadToEnd().ToString();
    //                    reader.Close();
    //                }
    //            }
    //            return responseData;
    //        }
    //        catch
    //        {
    //            throw;
    //        }
    //        finally
    //        {
    //            try
    //            {
    //                response.Close();
    //                GC.Collect();
    //            }
    //            catch
    //            {
    //            }
    //        }
    //        //return responseData;

    //    }
    //    private static string RequestURLPost(string url, string postData)
    //    {
    //        string responseData = "";
    //        HttpWebResponse response = null;
    //        try
    //        {
    //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    //            request.Method = "POST";
    //            request.ContentType = "application/x-www-form-urlencoded";
    //            request.Timeout = 0x7530;
    //            byte[] bs = new UTF8Encoding().GetBytes(postData);
    //            request.ContentLength = bs.Length;
    //            using (Stream reqStream = request.GetRequestStream())
    //            {
    //                reqStream.Write(bs, 0, bs.Length);
    //                reqStream.Flush();
    //                reqStream.Close();
    //            }
    //            response = (HttpWebResponse)request.GetResponse();
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
    //                {
    //                    responseData = reader.ReadToEnd().ToString();
    //                    reader.Close();
    //                }
    //            }
    //            return responseData;
    //        }
    //        catch
    //        {
    //            throw;
    //        }
    //        finally
    //        {
    //            try
    //            {
    //                response.Close();
    //                GC.Collect();
    //            }
    //            catch
    //            {
    //            }
    //        }
    //        //return responseData;

    //    }
    //    public static bool SendSms(string phone, string message)
    //    {
    //        string sendMessUrl = "http://sapi.253.com/msg/HttpBatchSendSM";
    //        string sendParam = "account={0}&pswd={1}&mobile={2}&msg={3}&needstatus=true&extno=";
    //        try
    //        {
    //            object[] args = new object[] { "buzz168", "Buzztime666", phone, message };
    //            sendParam = string.Format(sendParam, args);
    //            XMS.Core.Container.LogService.Info("短信发送信息：" + sendMessUrl);
    //            string result = RequestURL(sendMessUrl, sendParam);
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }


    //}

    public class HttpHelper
    {
        public static string RequestURL(string url, string postData)
        {
            bool flag = string.IsNullOrEmpty(postData);
            string result;
            if (flag)
            {
                result = HttpHelper.RequestURLGet(url);
            }
            else
            {
                result = HttpHelper.RequestURLPost(url, postData);
            }
            return result;
        }
        private static string RequestURLGet(string url)
        {
            string text = "";
            HttpWebResponse httpWebResponse = null;
            string result;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                httpWebRequest.Timeout = 30000;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                bool flag = httpWebResponse.StatusCode == HttpStatusCode.OK;
                if (flag)
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        text = streamReader.ReadToEnd().ToString();
                        streamReader.Close();
                    }
                }
                result = text;
            }
            catch
            {
                throw;
            }
            finally
            {
                try
                {
                    httpWebResponse.Close();
                    GC.Collect();
                }
                catch
                {
                }
            }
            return result;
        }
        private static string RequestURLPost(string url, string postData)
        {
            string text = "";
            HttpWebResponse httpWebResponse = null;
            string result;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Timeout = 30000;
                byte[] bytes = new UTF8Encoding().GetBytes(postData);
                httpWebRequest.ContentLength = (long)bytes.Length;
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                bool flag = httpWebResponse.StatusCode == HttpStatusCode.OK;
                if (flag)
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        text = streamReader.ReadToEnd().ToString();
                        streamReader.Close();
                    }
                }
                result = text;
            }
            catch
            {
                throw;
            }
            finally
            {
                try
                {
                    httpWebResponse.Close();
                    GC.Collect();
                }
                catch
                {
                }
            }
            return result;
        }
        public static bool SendSms(string phone, string message)
        {
            string text = "http://sapi.253.com/msg/HttpBatchSendSM";
            string text2 = "account={0}&pswd={1}&mobile={2}&msg={3}&needstatus=true&extno=";
            bool result;
            try
            {
                object[] args = new object[]
                {
                    "buzz168",
                    "Buzztime666",
                    phone,
                    message
                };
                text2 = string.Format(text2, args);
                
                string text3 = HttpHelper.RequestURL(text, text2);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }

}