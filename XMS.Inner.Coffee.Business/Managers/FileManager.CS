using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
using System.Data;
using System.IO;
using System.Xml;
using XMS.Core;
using System.Drawing;
//using Aliyun.OSS;

namespace XMS.Inner.Coffee.Business
{
    public class FileManager
    {
        public static Regex _regRegexFilePath = new Regex(@"(\\\w+)*(\w+)", RegexOptions.Compiled);                                         //文件路径的正则
        public static Regex _regPicFormat = new Regex(@"[0-9]*,[0-9]*(,(true)|(false)){0,1}", RegexOptions.Compiled);                       //缩略图的正则
        public static string WaterFile = AppSettingHelper.WaterPicFilePath;  //水印图片的位置

        public static readonly FileManager Instance = new FileManager();
        private FileManager() { }

        /// <summary>
        /// 上传图片主方法
        /// </summary>
        /// <param name="picData">图片流</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="rootKey">配置文件中Key值</param>
        /// <param name="isThumbnail">是否需要缩略图</param>
        /// <returns></returns>
        public PhotoResult UploadPic(byte[] picData, string filePath, bool autoControl)
        {
            //记录上传阿里云的数据
            List<string> lstPath = new List<string>();

            string rootPath = @"C:\Web\AppWapCoffee\UploadImage";

            PhotoResult ps = new PhotoResult();
            Regex rgDisk = new Regex(@"^(.*)[a-zA-Z]:[/\\]", RegexOptions.Compiled);
            filePath = rgDisk.Replace(filePath, "");
            if (picData == null || picData.Length > AppSettingHelper.FileMaxSize)
            {
                throw new ArgumentNullException("上传文件为空或者超过限定大小");
            }
            filePath = "/" + filePath.Replace("\\\\", "/").Replace("\\", "/").Replace("//", "/").TrimStart('/'); //将 双正斜杠，双反斜杠，反斜杠都变成 正斜杠
            if (!_regRegexFilePath.IsMatch(filePath))
            {
                throw new ArgumentNullException("文件路径错误，标准格式为(/[a-z0-9A-Z]+/)*[a-z0-9A-Z]+");
            }
            //if (RealRule[rootKey] == null)
            //{
            //    throw new ArgumentNullException("未存在的上传规则配置项");
            //}
            //Rule objCurrentRule = (Rule)RealRule[rootKey];
            //bool bIsWaterMake = objCurrentRule.IsWaterMark;
            //string sWaterFile = objCurrentRule.WaterFilePath;
            //string watermarkPosition = objCurrentRule.WatermarkPosition;
            string _sDateDir = DateTime.Now.ToString("yyyyMMdd") + "/";
            //传入的文件夹
            string _sFileFoder = (filePath.LastIndexOf('/') >= 0) ? filePath.Substring(0, filePath.LastIndexOf('/')).TrimStart('/').TrimEnd('/') + "/" : String.Empty;
            //传入的文件名
            string _sFileName = (filePath.LastIndexOf('/') >= 0) ? filePath.Substring(filePath.LastIndexOf('/') + 1) : filePath;
            //传入的文件类型
            string _sFileType = (_sFileName.LastIndexOf(".") > 0) ? _sFileName.Substring(_sFileName.LastIndexOf(".")).ToLower() : String.Empty;
            //目标文件夹
            string _sDir = rootPath.TrimEnd('/') + "/" + _sDateDir + _sFileFoder;
            //if (!objCurrentRule.IsAddDateFolder)
            //{
            //    _sDir = objCurrentRule.RootPath.TrimEnd('/') + "/" + _sFileFoder;
            //}

            //png图片不需要打水印，否则会将水印的背景色遮住原有图片
            //if (_sFileType.Equals(".png", StringComparison.CurrentCultureIgnoreCase))
            //{
            //    bIsWaterMake = false;
            //}

            //目标文件名
            string _sNewFileName = _sFileName;
            if (autoControl)
            {
                _sNewFileName = System.Guid.NewGuid().ToString() + _sFileType;
            }

            string _sBigFile = _sDir + _sNewFileName; //大图地址
            string _sOriginalFile = string.Empty;     //原始图

            if (!Directory.Exists(_sDir))
            {
                Directory.CreateDirectory(_sDir);
            }
            ps.Md5 = PicHandle.GetMd5Hash(picData);
            using (FileStream stream = new FileStream(_sBigFile, FileMode.OpenOrCreate))
            {
                XMS.Core.Container.LogService.Error("_sBigFile:" + _sBigFile);
                lstPath.Add(_sBigFile);
                stream.Write(picData, 0, picData.Length);
                stream.Close();
            }
            //if (".jpg".Equals(_sFileType, StringComparison.CurrentCultureIgnoreCase)
            //    || ".jpeg".Equals(_sFileType, StringComparison.CurrentCultureIgnoreCase)
            //    || ".gif".Equals(_sFileType, StringComparison.CurrentCultureIgnoreCase)
            //    || ".png".Equals(_sFileType, StringComparison.CurrentCultureIgnoreCase)
            // )
            //{
            //    byte[] barrImgData = System.IO.File.ReadAllBytes(_sBigFile);
            //    MemoryStream ms = new MemoryStream(barrImgData);
            //    Image originalImage = null;
            //    try
            //    {
            //        originalImage = Image.FromStream(ms);
            //        ps.Width = originalImage.Width;
            //        ps.Height = originalImage.Height;
            //        //根据图片大小来选择水印图片
            //        if (ps.Width <= 320 || originalImage.Height <= 240)
            //        {
            //            if (System.IO.File.Exists(sWaterFile.Replace(".png", "-small.png")))
            //            {
            //                sWaterFile = sWaterFile.Replace(".png", "-small.png");
            //            }
            //        }
            //        PicGeoHelper.FloatPoint point = PicGeoHelper.GetPoint(originalImage);
            //        if (point != null)
            //        {
            //            ps.Longitude = point.Longitude;
            //            ps.Latitude = point.Latitude;
            //            ps.PhotoTime = point.PhotoTime;
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        if (originalImage != null) { originalImage.Dispose(); }
            //        XMS.Core.Container.LogService.Error("读取图片信息出错：" + e.ToString());
            //    }
            //    finally
            //    {
            //        if (originalImage != null)
            //        {
            //            originalImage.Dispose();
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(objCurrentRule.MaxSize))
            //    {
            //        try
            //        {
            //            string[] MaxSizeFormat = objCurrentRule.MaxSize.Split(',');
            //            if (MaxSizeFormat.Length >= 2)
            //            {
            //                int nMaxWidth = Convert.ToInt32(MaxSizeFormat[0]);
            //                int nMaxHeight = Convert.ToInt32(MaxSizeFormat[1]);
            //                if (nMaxWidth < ps.Width || nMaxHeight < ps.Height)//如果宽或高超过了就截图
            //                {
            //                    string sTempOriginalFile = _sDir + "TempOriginal/" + _sNewFileName;//设置一个临时的目录文件
            //                    if (PicHandle.CreateThumbnailImage(_sBigFile, sTempOriginalFile, nMaxWidth, nMaxHeight))//截成理想大小成功
            //                    {
            //                        if (System.IO.File.Exists(sTempOriginalFile))
            //                        {
            //                            System.IO.File.Delete(_sBigFile);
            //                            System.IO.File.Move(sTempOriginalFile, _sBigFile);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception exp)
            //        {
            //            XMS.Core.Container.LogService.Error(exp.ToString());
            //        }
            //    }
            //    //存放原图
            //    if (!Directory.Exists(_sDir + "Original/"))
            //    {
            //        Directory.CreateDirectory(_sDir + "Original/");
            //    }
            //    _sOriginalFile = _sDir + "Original/" + _sNewFileName;          //更改原图地址
            //    using (FileStream stream = new FileStream(_sOriginalFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //    {
            //        XMS.Core.Container.LogService.Error("_sOriginalFile:" + _sOriginalFile);
            //        lstPath.Add(_sOriginalFile);
            //        stream.Write(picData, 0, picData.Length);
            //        stream.Close();
            //    }

            //    //缩略图 
            //    if (isThumbnail)
            //    {
            //        if (!String.IsNullOrEmpty(objCurrentRule.Small))
            //        {
            //            string[] Format = objCurrentRule.Small.Split(',');
            //            PicHandle.CreateThumbnailImage(_sBigFile, _sDir + "Small/" + _sNewFileName, Convert.ToInt32(Format[0]), Convert.ToInt32(Format[1]));
            //            if (Format.Length == 3 && Format[2].ToLower() == "true" && System.IO.File.Exists(sWaterFile))
            //            {
            //                PicHandle.MarkWater(_sDir + "Small/" + _sNewFileName, sWaterFile, watermarkPosition);
            //            }
            //            XMS.Core.Container.LogService.Error("Small:" + _sDir + "Small/" + _sNewFileName);
            //            lstPath.Add(_sDir + "Small/" + _sNewFileName);
            //        }
            //        if (!String.IsNullOrEmpty(objCurrentRule.Middle))
            //        {
            //            string[] Format = objCurrentRule.Middle.Split(',');
            //            PicHandle.CreateThumbnailImage(_sBigFile, _sDir + "Middle/" + _sNewFileName, Convert.ToInt32(Format[0]), Convert.ToInt32(Format[1]));
            //            if (Format.Length == 3 && Format[2].ToLower() == "true" && System.IO.File.Exists(sWaterFile))
            //            {
            //                PicHandle.MarkWater(_sDir + "Middle/" + _sNewFileName, sWaterFile, watermarkPosition);
            //            }
            //            XMS.Core.Container.LogService.Error("Middle:" + _sDir + "Middle/" + _sNewFileName);
            //            lstPath.Add(_sDir + "Middle/" + _sNewFileName);
            //        }
            //        if (!String.IsNullOrEmpty(objCurrentRule.Big))
            //        {
            //            string[] Format = objCurrentRule.Big.Split(',');
            //            PicHandle.CreateThumbnailImage(_sBigFile, _sDir + "Big/" + _sNewFileName, Convert.ToInt32(Format[0]), Convert.ToInt32(Format[1]));
            //            if (Format.Length == 3 && Format[2].ToLower() == "true" && System.IO.File.Exists(sWaterFile))
            //            {
            //                PicHandle.MarkWater(_sDir + "Big/" + _sNewFileName, sWaterFile, watermarkPosition);
            //            }
            //            XMS.Core.Container.LogService.Error("Big:" + _sDir + "Big/" + _sNewFileName);
            //            lstPath.Add(_sDir + "Big/" + _sNewFileName);
            //        }
            //        //其他N种缩略图
            //        if (objCurrentRule.Others != null && objCurrentRule.Others.Count > 0)
            //        {
            //            foreach (Rule.ThumbnailImg objImg in objCurrentRule.Others)
            //            {
            //                if (objImg.Width > 0 || objImg.Height > 0)
            //                {
            //                    string sFolder = string.Format("{0}_{1}", objImg.Width, objImg.IsAutoThumbWhenWidthEqualsHeight ? "x" : objImg.Height.ToString());
            //                    //生成缩略图
            //                    PicHandle.CreateThumbnailImage(_sBigFile, _sDir + sFolder + "/" + _sNewFileName, objImg.Width, objImg.Height, objImg.IsAutoThumbWhenWidthEqualsHeight);
            //                    if (bIsWaterMake && objImg.IsWaterMark && System.IO.File.Exists(sWaterFile))  //需要打水印
            //                    {
            //                        PicHandle.MarkWater(_sDir + sFolder + "/" + _sNewFileName, sWaterFile, watermarkPosition);
            //                    }
            //                    XMS.Core.Container.LogService.Error("sFolder:" + _sDir + sFolder + "/" + _sNewFileName);
            //                    lstPath.Add(_sDir + sFolder + "/" + _sNewFileName);
            //                }
            //            }
            //        }
            //    }
            //    //给大图打水印
            //    if (bIsWaterMake && System.IO.File.Exists(sWaterFile))
            //    {
            //        PicHandle.MarkWater(_sBigFile, sWaterFile, watermarkPosition);   //上传图片加上水印                        
            //    }

            //    //修改原图名称
            //    if (System.IO.File.Exists(_sDir + "Original/" + _sNewFileName))
            //    {
            //        string sMoveToFile = _sDir + "Original/" + _sNewFileName + ".config";
            //        try
            //        {
            //            if (System.IO.File.Exists(sMoveToFile))
            //            {
            //                System.IO.File.Delete(sMoveToFile);
            //            }
            //            System.IO.File.Move(_sDir + "Original/" + _sNewFileName, sMoveToFile);
            //            System.IO.File.Delete(_sDir + "Original/" + _sNewFileName);
            //        }
            //        catch (Exception exp)
            //        {
            //            XMS.Core.Container.LogService.Error(exp.ToString());
            //        }
            //    }
            //    if (!String.IsNullOrEmpty(objCurrentRule.ReplaceOriginalFile))
            //    {
            //        try
            //        {
            //            string sReplaceFile = _sDir + objCurrentRule.ReplaceOriginalFile + "/" + _sNewFileName;
            //            if (System.IO.File.Exists(sReplaceFile))
            //            {
            //                System.IO.File.Delete(_sDir + "/" + _sNewFileName);
            //                System.IO.File.Move(sReplaceFile, _sDir + "/" + _sNewFileName);
            //            }
            //        }
            //        catch { }
            //    }
            //}
            string _sVirtualPath = "http://waimai.buzztimecoffee.com/AppWapCoffee/UploadImage".TrimStart('/').TrimEnd('/') + "/" + _sDateDir + _sFileFoder;
            //if (!objCurrentRule.IsAddDateFolder)
            //{
            //    _sVirtualPath = objCurrentRule.VirtualPath.TrimStart('/').TrimEnd('/') + "/" + _sFileFoder.TrimStart('/');
            //}
            ps.Path = _sVirtualPath.TrimStart('/').TrimEnd('/') + "/" + _sNewFileName;
            ps.Url = ps.Path;
            //lstPath.Add(objCurrentRule.RootPath);
            //DoUploadFileToOss(lstPath, objCurrentRule.VirtualPath);
            //Thread t = new Thread(new ParameterizedThreadStart(DoUploadFileToOss));
            //t.IsBackground = true;
            //t.Start(lstPath);
            return ps;
        }

        private static string AliYunAccessKeyId
        {
            get
            {
                string accessKeyId = System.Configuration.ConfigurationManager.AppSettings["AliYunAccessKeyId"];
                if (String.IsNullOrEmpty(accessKeyId))
                {
                    accessKeyId = "";
                }
                return accessKeyId;
            }
        }
        private static string AliYunAccessKeySecret
        {
            get
            {
                string accessKeySecret = System.Configuration.ConfigurationManager.AppSettings["AliYunAccessKeySecret"];
                if (String.IsNullOrEmpty(accessKeySecret))
                {
                    accessKeySecret = "";
                }
                return accessKeySecret;
            }
        }
        private static string AliYunEndpoint
        {
            get
            {
                string endpoint = System.Configuration.ConfigurationManager.AppSettings["AliYunEndpoint"];
                if (String.IsNullOrEmpty(endpoint))
                {
                    endpoint = "";
                }
                return endpoint;
            }
        }


        //public static readonly OssClient ossClient = new OssClient(AliYunEndpoint, AliYunAccessKeyId, AliYunAccessKeySecret);
        //public void DoUploadFileToOss(List<string> list, string sVirtualPath)
        //{
        //    try
        //    {
        //        if (list == null || list.Count == 0)
        //        {
        //            Container.LogService.Error("list is null");
        //            return;
        //        }
        //        string rootPath = list[list.Count - 1];
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            try
        //            {
        //                if (i >= list.Count - 1)
        //                {
        //                    break;
        //                }

        //                string item = list[i];
        //                //if (item.ToLower().Contains("original"))
        //                //{
        //                //    continue;
        //                //}
        //                string fileToUpload = item.Replace("//", "\\").Replace("/", "\\");

        //                string key = fileToUpload.Replace(rootPath, "").Replace("\\", "/").DoTrimStart('/');
        //                key = sVirtualPath.DoTrimStart('/') + "/" + key;
        //                key = key.DoTrimStart('/');
        //                Container.LogService.Error("key:" + key);
        //                Container.LogService.Error("fileToUpload:" + fileToUpload);
        //                ossClient.PutObject("xms-upload", key, fileToUpload);
        //            }
        //            catch (Exception e)
        //            {
        //                Container.LogService.Error(e.ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Container.LogService.Error(ex.ToString());
        //    }

        //}

        public Hashtable RealRule                    //实时的规则设定
        {
            get
            {
                if (HttpRuntime.Cache["RealRule"] == null)
                {
                    Hashtable _htRealRule = new Hashtable();
                    DataSet _dsConfig = new DataSet();
                    string sFileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "conf/config.xml");
                    if (!System.IO.File.Exists(sFileName)) return null;
                    try
                    {
                        _dsConfig.ReadXml(sFileName);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(sFileName);
                        if (_dsConfig.Tables["Rule"] != null && _dsConfig.Tables["Rule"].Rows.Count > 0)
                        {
                            foreach (DataRow _objDrow in _dsConfig.Tables["Rule"].Rows)
                            {
                                string _sSmall = string.Empty;
                                string _sMiddle = string.Empty;
                                string _sBig = string.Empty;
                                string _sMaxSize = string.Empty;
                                string _sWaterFilePath = string.Empty;
                                string _sWatermarkPosition = string.Empty;
                                try
                                {
                                    _sSmall = _objDrow["Small"].ToString();
                                    _sMiddle = _objDrow["Middle"].ToString();
                                    _sBig = _objDrow["Big"].ToString();
                                    _sMaxSize = _objDrow["MaxSize"].ToString();
                                }
                                catch (Exception exp)
                                {
                                    //Logger.Error(exp.ToString());// MyLog.MyLog.WriteWrongLog(exp.ToString());\
                                    XMS.Core.Container.LogService.Error(exp.ToString());
                                }
                                //自己配置的水印
                                try
                                {
                                    _sWaterFilePath = _objDrow["WaterFile"].ToString();
                                    _sWatermarkPosition = _objDrow["WatermarkPosition"].ToString();
                                }
                                catch { }

                                Rule _objRule = new Rule();
                                _objRule.IsAddDateFolder = true;
                                _objRule.RootPath = _objDrow["RootPath"].ToString();
                                _objRule.VirtualPath = _objDrow["VirtualPath"].ToString();
                                _objRule.IsWaterMark = _objDrow["IsWaterMark"].ConvertTo<bool>(false);
                                try
                                {
                                    if (_objDrow["IsAddDateFolder"] != null && !String.IsNullOrEmpty(_objDrow["IsAddDateFolder"].ToString()))
                                    {
                                        _objRule.IsAddDateFolder = _objDrow["IsAddDateFolder"].ConvertTo<bool>(false);
                                    }
                                }
                                catch (Exception exp)
                                {
                                    XMS.Core.Container.LogService.Error(exp.ToString());// MyLog.MyLog.WriteWrongLog(exp.ToString());
                                }
                                try
                                {
                                    _objRule.ReplaceOriginalFile = Convert.ToString(_objDrow["ReplaceOriginalFile"]);
                                    _objRule.WaterFileSmallPath = Convert.ToString(_objDrow["WaterFileSmall"]);
                                }
                                catch (Exception exp)
                                {
                                    XMS.Core.Container.LogService.Error(exp.ToString());
                                }

                                if (!String.IsNullOrEmpty(_sSmall))
                                { _objRule.Small = _regPicFormat.IsMatch(_sSmall) ? _sSmall : String.Empty; }
                                else
                                { _objRule.Small = String.Empty; }

                                if (!String.IsNullOrEmpty(_sMiddle))
                                { _objRule.Middle = _regPicFormat.IsMatch(_sMiddle) ? _sMiddle : String.Empty; }
                                else
                                { _objRule.Middle = String.Empty; }

                                _objRule.Big = _sBig;

                                //赋值图片的最大尺寸
                                if (!String.IsNullOrEmpty(_sMaxSize))
                                { _objRule.MaxSize = _regPicFormat.IsMatch(_sMaxSize) ? _sMaxSize : String.Empty; }
                                else
                                { _objRule.MaxSize = String.Empty; }

                                _objRule.AutoControl = _objDrow["AutoControl"].ConvertTo<bool>(false);
                                _objRule.WaterFilePath = string.IsNullOrEmpty(_sWaterFilePath) ? WaterFile : _sWaterFilePath;
                                _objRule.WatermarkPosition = _sWatermarkPosition;
                                if (!string.IsNullOrEmpty(_objRule.WaterFilePath) && !_objRule.WaterFilePath.Contains(":"))
                                {
                                    _objRule.WaterFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _objRule.WaterFilePath);
                                }

                                //取其他类型的图片
                                XmlNodeList xmlNodes = xmlDoc.SelectNodes(string.Format("root/Rule[@RootKey='{0}']/pic", _objDrow["RootKey"].ToString()));
                                if (xmlNodes.Count > 0)
                                {
                                    List<Rule.ThumbnailImg> lstOthers = new List<Rule.ThumbnailImg>();
                                    foreach (XmlNode node in xmlNodes)
                                    {
                                        if (!string.IsNullOrEmpty(node.InnerText))
                                        {
                                            string[] arrOther = node.InnerText.Split(new char[] { ',', '，' });
                                            if (arrOther.Length >= 2 && arrOther.Length <= 4)
                                            {
                                                Rule.ThumbnailImg objImg = new Rule.ThumbnailImg();
                                                objImg.Width = Convert.ToInt32(arrOther[0]);
                                                objImg.Height = Convert.ToInt32(arrOther[1]);
                                                if (arrOther.Length > 2)
                                                {
                                                    objImg.IsWaterMark = arrOther[2].ConvertToBoolean(); ;
                                                }
                                                //objImg.IsWaterMark = arrOther.Length == 2 ? false : arrOther[2].ToLower() == "true";
                                                if (arrOther.Length > 3)
                                                {
                                                    objImg.IsAutoThumbWhenWidthEqualsHeight = arrOther[3].ConvertToBoolean(); ;
                                                }
                                                lstOthers.Add(objImg);
                                            }
                                        }
                                    }
                                    _objRule.Others = lstOthers;
                                }


                                //添加入哈希表
                                _htRealRule[Convert.ToString(_objDrow["RootKey"])] = _objRule;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XMS.Core.Container.LogService.Error(ex.ToString());
                    }
                    HttpRuntime.Cache.Insert("RealRule", _htRealRule, new System.Web.Caching.CacheDependency(sFileName));
                }
                return (Hashtable)HttpRuntime.Cache["RealRule"];
            }
        }


        public class Rule
        {
            // 物理路径根目录支持 Demo 运行模式 edit by 翟雪东 2011-12-2
            // 暂时使用直接读取 Header 的方式判断是否为 demo 模式，将来可考虑使用 XMS.Core 来进行支持 
            //public string RootPath { get; set; } //物理路径根目录
            //public string VirtualPath { get; set; }         //对应虚拟目录
            private string rootPath;
            public string RootPath
            {
                get
                {
                    // 服务环境中根据当前服务中的 demo 标头决定是否访问 Demo 版
                    System.ServiceModel.OperationContext context = System.ServiceModel.OperationContext.Current;
                    if (context != null)
                    {
                        int headerIndex = context.IncomingMessageHeaders.FindHeader("demo", String.Empty);
                        if (headerIndex >= 0)
                        {
                            if (context.IncomingMessageHeaders.GetHeader<bool>(headerIndex))
                            {
                                return this.rootPath + "/demo";
                            }
                        }
                    }
                    return this.rootPath;
                }
                set
                {
                    this.rootPath = value == null ? null : value.TrimEnd('/');
                }
            }
            private string virtualPath;
            public string VirtualPath
            {
                get
                {
                    // 服务环境中根据当前服务中的 demo 标头决定是否访问 Demo 版
                    System.ServiceModel.OperationContext context = System.ServiceModel.OperationContext.Current;
                    if (context != null)
                    {
                        int headerIndex = context.IncomingMessageHeaders.FindHeader("demo", String.Empty);
                        if (headerIndex >= 0)
                        {
                            if (context.IncomingMessageHeaders.GetHeader<bool>(headerIndex))
                            {
                                return this.virtualPath + "/demo";
                            }
                        }
                    }
                    return this.virtualPath;
                }
                set
                {
                    this.virtualPath = value == null ? null : value.TrimEnd('/');
                }
            }
            public bool IsWaterMark { get; set; }           //是否需要水印
            public bool AutoControl { get; set; }           //是否自动控制（文件路径自主权）
            public bool IsAddDateFolder { get; set; }       //是否自动添加日期文件目录
            public string Small { get; set; }               //小图格式 width,height,iswatermaker
            public string Middle { get; set; }              //中图格式 width,height,iswatermaker
            public string Big { get; set; }                 //大图格式 width,height,iswatermaker
            public List<ThumbnailImg> Others { get; set; }  //其它缩略图列表
            public string WaterFilePath { get; set; }       //各配置节自己的水印图位置
            public string WatermarkPosition { get; set; }   //水印所在底图的位置
            public string WaterFileSmallPath { get; set; }  //图片宽高度小于指定宽高度采用的水印图
            public string ReplaceOriginalFile { get; set; } //从缩略图列表中 替换原图
            public string MaxSize { get; set; }             //限定图片的最大尺寸


            /// <summary>
            /// 缩略图
            /// </summary>
            public class ThumbnailImg
            {
                /// <summary>
                /// 宽度
                /// </summary>
                public int Width { get; set; }
                /// <summary>
                /// 高度
                /// </summary>
                public int Height { get; set; }
                /// <summary>
                /// 是否打水印
                /// </summary>
                public bool IsWaterMark { get; set; }

                /// <summary>
                /// 水印文件路径
                /// </summary>
                public string WaterMarkFile { get; set; }

                /// <summary>
                /// 缩略图宽高相等时是否自动选择大的一边等比压缩
                /// </summary>
                public bool IsAutoThumbWhenWidthEqualsHeight { get; set; }
            }
        }
    }
}
