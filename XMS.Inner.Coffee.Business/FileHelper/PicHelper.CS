using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace XMS.Inner.Coffee.Business
{
    public static class PicHandle
    {
        public static readonly int MaxLength = 4;
        public static readonly string SessionKey = "GengerP$#";
        public static bool IsValidString(string key)
        {
            if (String.IsNullOrEmpty(key)) return false;
            string args = System.Web.HttpContext.Current.Session[SessionKey] as string;
            if (String.IsNullOrEmpty(args)) return false;
            if (key.ToLower().Trim().Equals(args.ToLower()))
            {
                System.Web.HttpContext.Current.Session[SessionKey] = null;
                return true;
            }
            return false;
        }
        public static string GetString()
        {
            int number;
            char code;
            string checkCode = String.Empty;
            System.Random random = new Random();
            for (int i = 1; i <= MaxLength; i++)
            {
                number = random.Next();
                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else if (number % 3 == 0)
                    code = (char)('a' + (char)(number % 26));
                else
                    code = (char)('A' + (char)(number % 26));
                if ((code == '0') || (code == 'i') || (code == 'I') || (code == 'O') || (code == 'o') || (code == '1') || (code == 'l'))
                {
                    --i;
                    continue;
                }
                checkCode += code.ToString();
            }
            System.Web.HttpContext.Current.Session[SessionKey] = checkCode;
            return checkCode;
        }
        public static bool CreateThumbnailImage(string sourcefile, string destinationfile, int nWidth, int nHeight)
        {
            return CreateThumbnailImage(sourcefile, destinationfile, nWidth, nHeight, false);
        }


        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="sourcefile">原图路径</param>
        /// <param name="destinationfile">缩略图路径</param>
        /// <param name="nWidth">缩略图宽度</param>
        /// <param name="nHeight">缩略图高度</param>
        /// <param name="bAutoThumbWhenWidthEqualsHeight">缩略图宽高相等时是否根据大的一边等比缩放</param>
        /// <returns></returns>
        public static bool CreateThumbnailImage(string sourcefile, string destinationfile, int nWidth, int nHeight, bool bAutoThumbWhenWidthEqualsHeight)
        {
            bool isCutLogo = false;

            if ((nWidth == 699 && nHeight == 0)
                || (nWidth == 239 && nHeight == 239)
                )
            {
                isCutLogo = true;
            }


            //图片放大，将变得不清晰，所以不考虑放大效果。只有缩略图.
            byte[] barrImgData = System.IO.File.ReadAllBytes(sourcefile);
            Image originalImage = null;
            MemoryStream ms = new MemoryStream(barrImgData);

            try
            {
                originalImage = Image.FromStream(ms);
            }
            catch
            {
                if (originalImage != null)
                    originalImage.Dispose();
                return false;
            }
            finally
            {
                ms.Dispose();
            }

            bool isCreate = false;


            //699特殊逻辑
            if (isCutLogo)
            {
                System.Drawing.Image tmpBitMap = new System.Drawing.Bitmap(originalImage.Width, originalImage.Height - 50);
                Graphics tmpG = null;
                tmpG = System.Drawing.Graphics.FromImage(tmpBitMap);
                tmpG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                tmpG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                tmpG.Clear(Color.Transparent);
                //产生切割图
                tmpG.DrawImage(originalImage, new Rectangle(0, 0, tmpBitMap.Width, tmpBitMap.Height),
                    new Rectangle(0, 0, tmpBitMap.Width, tmpBitMap.Height), GraphicsUnit.Pixel);
                originalImage = tmpBitMap;
                tmpG.Dispose();
            }

            //图片原始大小
            int originalWidth = originalImage.Width;
            int originalHeight = originalImage.Height;

            //宽高相等时是否根据大的一边等比缩放
            if (bAutoThumbWhenWidthEqualsHeight && nWidth == nHeight)
            {
                if (originalImage.Width > originalImage.Height)
                {
                    nHeight = 0;
                }
                else
                {
                    nWidth = 0;
                }
            }

            //控制目标图片的大小范围，宽高都小于500的做拉伸，以便前台显示
            if (nWidth > 500 || nHeight > 500)
            {
                if (nWidth > originalWidth)
                    nWidth = originalWidth;
                else if (nWidth < 0)
                    nWidth = 0;
                if (nHeight > originalHeight)
                    nHeight = originalHeight;
                else if (nHeight < 0)
                    nHeight = 0;
            }

            int thumbnailW = 0, thumbnailH = 0;                     //缩略图大小
            if (nWidth == 0 && nHeight == 0)                        //原图大小
            {
                nWidth = originalWidth;
                nHeight = originalHeight;
            }
            else if (nWidth == 0)  //按高度等比
            {
                thumbnailH = nHeight;
                thumbnailW = (int)(((double)originalWidth / originalHeight) * nHeight);
            }
            else if (nHeight == 0)   //按宽度等比
            {
                thumbnailW = nWidth;
                thumbnailH = (int)(((double)originalHeight / originalWidth) * nWidth);
            }
            else
            {
                if (((double)originalWidth / originalHeight) >= ((double)nWidth / nHeight)) //长宽都有输入 选择差比最小的为标准
                {
                    thumbnailH = nHeight;
                    thumbnailW = (int)(((double)originalWidth / originalHeight) * nHeight);
                }
                else
                {
                    thumbnailW = nWidth;
                    thumbnailH = (int)(((double)originalHeight / originalWidth) * nWidth);
                }
            }
            System.Drawing.Image bitmap = null;
            Graphics g = null;

            //产生缩率图（如果高宽都不一致）
            if (thumbnailH != 0 && thumbnailW != 0)
            {
                int x = 0, y = 0;
                int w = thumbnailW, h = thumbnailH;
                //如果输入参数中宽高都有，并且高宽某一个属性达标后，切割图片
                if ((thumbnailH >= nHeight || thumbnailW >= nWidth) && nWidth > 0 && nHeight > 0)
                {
                    w = nWidth;
                    h = nHeight;

                    x = (thumbnailW == nWidth) ? 0 : (int)(originalWidth - (double)nWidth / nHeight * originalHeight) / 2;
                    y = (thumbnailH == nHeight) ? 0 : (int)(originalHeight - (double)originalWidth / nWidth * nHeight) / 2;
                }

                bitmap = new System.Drawing.Bitmap(w, h);
                g = System.Drawing.Graphics.FromImage(bitmap);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(Color.Transparent);
                //产生缩略图
                g.DrawImage(originalImage, new Rectangle(0, 0, w, h), new Rectangle(x, y, originalWidth - 2 * x, originalHeight - 2 * y), GraphicsUnit.Pixel);
            }

            if (bitmap == null)
            {
                bitmap = originalImage;
            }
            try
            {
                isCreate = true;

                //创建文件夹
                if (!Directory.Exists(Path.GetDirectoryName(destinationfile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationfile));

                string fileExtension = Path.GetExtension(destinationfile);
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 85L);
                EncoderParameters myEncoderParameters = new EncoderParameters(1); ;

                myEncoderParameters.Param[0] = myEncoderParameter;
                bitmap.Save(destinationfile, GetEncoderInfo(fileExtension), myEncoderParameters);
            }
            catch (Exception exp)
            {
                XMS.Core.Container.LogService.Error(exp.ToString());
                isCreate = false;
            }
            finally
            {
                if (originalImage != null)
                    originalImage.Dispose();
                if (bitmap != null)
                    bitmap.Dispose();
                if (g != null)
                    g.Dispose();
            }
            return isCreate;
        }

        static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].FilenameExtension.IndexOf(mimeType, StringComparison.CurrentCultureIgnoreCase) > -1)
                    return encoders[j];
            }
            return null;
        }

        public static string GetSmallPath(string path)
        {
            if (String.IsNullOrEmpty(path) || path.IndexOf(".") < 0) return path;
            return path.Insert(path.IndexOf("."), "_small");
        }
        /// <summary>
        /// 输入新文件路径，判断文件夹，返回一个新的不重复地址
        /// </summary>
        /// <param name="tfullPath">文件路径</param>
        /// <param name="replaceUrl">需要模式匹配的字符串</param>
        /// <returns></returns>
        public static string GetLastPath(string tfullPath, string replaceUrl)
        {
            if (String.IsNullOrEmpty(tfullPath) || String.IsNullOrEmpty(replaceUrl)) return null;
            string fullPath = Path.Combine(replaceUrl, tfullPath);
            if (!System.IO.File.Exists(fullPath)) return fullPath;
            int loopFrom = 0;
            string exts = Path.GetExtension(fullPath).Trim().ToLower();
            string oldPath = fullPath.Trim().ToLower().Substring(0, fullPath.Length - exts.Length);
            oldPath = Regex.Replace(oldPath, @"\[\d*\]$", "");
            string Opath = fullPath;

            while (System.IO.File.Exists(oldPath + "[" + (++loopFrom).ToString() + "]" + exts)) { }
            string OFilePath = (oldPath + "[" + loopFrom.ToString() + "]" + exts);
            return OFilePath.ToLower().Replace(replaceUrl.ToLower(), "").Trim('/', '\\');
        }

        /// <summary>
        /// 判断是否是图片的流
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidPicture(Stream str)
        {
            if (null == str)
                return false;
            else
            {
                bool isTrue = false;
                using (System.Drawing.Image im = System.Drawing.Bitmap.FromStream(str))
                {
                    isTrue = (
                        im.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png) ||
                        im.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg) ||
                        im.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif) ||
                        im.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)
                        );
                }
                return isTrue;
            }
        }
        public static bool IsValidPicture(byte[] source)
        {
            return IsValidPicture(IOHelper.BytesToStream(source));
        }
        public static void MarkWater(string filepath, string waterfile, string watermarkPosition)
        {
            byte[] barrImgData = System.IO.File.ReadAllBytes(filepath);
            byte[] barrWaterImgData = System.IO.File.ReadAllBytes(waterfile);
            MemoryStream msImg = new MemoryStream(barrImgData);
            MemoryStream msWaterImg = new MemoryStream(barrWaterImgData);
            Image img = null;
            Image markImg = null;

            try
            {
                img = Image.FromStream(msImg);
                markImg = Image.FromStream(msWaterImg);
                float[][] ptsArray ={ 
                                            new float[] {1, 0, 0, 0, 0},
                                            new float[] {0, 1, 0, 0, 0},
                                            new float[] {0, 0, 1, 0, 0},
                                            new float[] {0, 0, 0, 1.0f, 0}, //注意：此处为0.0f为完全透明，1.0f为完全不透明
                                            new float[] {0, 0, 0, 0, 1}};
                ColorMatrix colorMatrix = new ColorMatrix(ptsArray);
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.SkipGrays, ColorAdjustType.Bitmap);
                Bitmap newBitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format48bppRgb);
                newBitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                Graphics g = Graphics.FromImage(newBitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

                Rectangle watermarkRectangle = PicHandle.GetWatermarkRectangle(img.Width, img.Height, markImg.Width, markImg.Height, watermarkPosition);
                g.DrawImage(markImg, watermarkRectangle, 0, 0, markImg.Width, markImg.Height, GraphicsUnit.Pixel, imageAttributes);
                g.Dispose();
                ImageFormat igf = img.RawFormat;
                if (img != null) img.Dispose();
                newBitmap.Save(filepath, igf);
                newBitmap.Dispose();
            }
            catch (Exception exp)
            {
                XMS.Core.Container.LogService.Error("水印出错：" + exp.ToString());
            }
            finally
            {
                if (img != null) img.Dispose();
                if (markImg != null) markImg.Dispose();
            }

        }

        /// <summary>
        /// 返回小图片路径
        /// </summary>
        /// <param name="sPath">原图片路径</param>
        /// <returns></returns>
        public static string GetSmallPicPath(string sPath)
        {
            if (string.IsNullOrEmpty(sPath) || sPath.IndexOf("/") < 0)
            {
                return sPath;
            }
            sPath = sPath.ToLower().StartsWith("http://") ? sPath : (AppSettingHelper.StaticUploadUrl + sPath.TrimStart('/'));
            return sPath.Insert(sPath.LastIndexOf("/"), "/small");

        }

        /// <summary>
        /// 返回中图片路径
        /// </summary>
        /// <param name="sPath">原图片路径</param>
        /// <returns></returns>
        public static string GetMiddlePicPath(string sPath)
        {
            if (string.IsNullOrEmpty(sPath) || sPath.IndexOf("/") < 0)
            {
                return sPath;
            }
            sPath = sPath.ToLower().StartsWith("http://") ? sPath : (AppSettingHelper.StaticUploadUrl + sPath.TrimStart('/'));
            return sPath.Insert(sPath.LastIndexOf("/"), "/middle");
        }

        /// <summary>
        /// 返回文件服务器下的图片地址
        /// </summary>
        /// <param name="sPath">图片地址</param>
        /// <returns></returns>
        public static string GetUplodPicPath(string sPath)
        {
            if (string.IsNullOrEmpty(sPath))
            {
                return sPath;
            }
            sPath = sPath.ToLower().StartsWith("http://") ? sPath : (AppSettingHelper.StaticUploadUrl + sPath.TrimStart('/'));
            return sPath;
        }

        /// <summary>
        /// 取文件服务器各种尺寸的图片地址
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="sSize"></param>
        /// <returns></returns>
        public static string GetUploadPicPath(string sPath, string sSize)
        {
            if (string.IsNullOrEmpty(sPath))
            {
                return sPath;
            }
            sPath = sPath.ToLower().StartsWith("http://") ? sPath : (AppSettingHelper.StaticUploadUrl + sPath.TrimStart('/'));
            if (!string.IsNullOrEmpty(sSize))
            {
                sPath = sPath.Insert(sPath.LastIndexOf("/"), "/" + sSize);
            }
            return sPath;
        }
        /// <summary>
        /// 获取Byte数组的MD5值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(byte[] input)
        {
            if (input == null)
            {
                return string.Empty;
            }
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(input);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
        /// </summary>
        /// <returns></returns>
        private static Rectangle GetWatermarkRectangle(int baseMapWidth, int baseMapHeight, int WatermarkWidth, int WatermarkHeight, string WatermarkPosition)
        {
            if (WatermarkWidth > (baseMapWidth - 5) || WatermarkHeight > (baseMapHeight - 5))
            {
                return new Rectangle(0, baseMapHeight - WatermarkHeight, WatermarkWidth, WatermarkHeight);
            }
            Rectangle result = new Rectangle();
            switch (WatermarkPosition.ToLower())
            {
                case "righttop":
                    result = PicHandle.GetRightTopRectangle(baseMapWidth, baseMapHeight, WatermarkWidth, WatermarkHeight);
                    break;
                case "rightbottom":
                    result = PicHandle.GetRightBottomRectangle(baseMapWidth, baseMapHeight, WatermarkWidth, WatermarkHeight);
                    break;
                case "lefttop":
                    result = PicHandle.GetLeftTopRectangle(baseMapWidth, baseMapHeight, WatermarkWidth, WatermarkHeight);
                    break;
                case "leftbottom":
                    result = PicHandle.GetLeftBottomRectangle(baseMapWidth, baseMapHeight, WatermarkWidth, WatermarkHeight);
                    break;
                case "center":
                    result = PicHandle.GetCenterRectangle(baseMapWidth, baseMapHeight, WatermarkWidth, WatermarkHeight);
                    break;
                default:
                    result = PicHandle.GetRightBottomRectangle(baseMapWidth, baseMapHeight, WatermarkWidth, WatermarkHeight);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取右上角矩形
        /// </summary>
        /// <param name="baseMapWidth">底图宽度</param>
        /// <param name="baseMapHeight">底图高度</param>
        /// <param name="WatermarkWidth">水印宽度</param>
        /// <param name="WatermarkHeight">水印高度</param>
        /// <returns></returns>
        private static Rectangle GetRightTopRectangle(int baseMapWidth, int baseMapHeight, int WatermarkWidth, int WatermarkHeight)
        {
            int markx = baseMapWidth - WatermarkWidth - 5;
            int marky = 5;
            return new Rectangle(markx, marky, WatermarkWidth, WatermarkHeight);
        }

        /// <summary>
        /// 获取右下角矩形
        /// </summary>
        /// <param name="baseMapWidth">底图宽度</param>
        /// <param name="baseMapHeight">底图高度</param>
        /// <param name="WatermarkWidth">水印宽度</param>
        /// <param name="WatermarkHeight">水印高度</param>
        /// <returns></returns>
        private static Rectangle GetRightBottomRectangle(int baseMapWidth, int baseMapHeight, int WatermarkWidth, int WatermarkHeight)
        {
            int markx = baseMapWidth - WatermarkWidth - 5;
            int marky = baseMapHeight - WatermarkHeight - 5;
            return new Rectangle(markx, marky, WatermarkWidth, WatermarkHeight);
        }

        /// <summary>
        /// 获取左上角矩形
        /// </summary>
        /// <param name="baseMapWidth">底图宽度</param>
        /// <param name="baseMapHeight">底图高度</param>
        /// <param name="WatermarkWidth">水印宽度</param>
        /// <param name="WatermarkHeight">水印高度</param>
        /// <returns></returns>
        private static Rectangle GetLeftTopRectangle(int baseMapWidth, int baseMapHeight, int WatermarkWidth, int WatermarkHeight)
        {
            int markx = 5;
            int marky = 5;
            return new Rectangle(markx, marky, WatermarkWidth, WatermarkHeight);
        }

        /// <summary>
        /// 获取左下角矩形
        /// </summary>
        /// <param name="baseMapWidth">底图宽度</param>
        /// <param name="baseMapHeight">底图高度</param>
        /// <param name="WatermarkWidth">水印宽度</param>
        /// <param name="WatermarkHeight">水印高度</param>
        /// <returns></returns>
        private static Rectangle GetLeftBottomRectangle(int baseMapWidth, int baseMapHeight, int WatermarkWidth, int WatermarkHeight)
        {
            int markx = 5;
            int marky = baseMapHeight - WatermarkHeight - 5;
            return new Rectangle(markx, marky, WatermarkWidth, WatermarkHeight);
        }

        /// <summary>
        /// 获取中间矩形
        /// </summary>
        /// <param name="baseMapWidth">底图宽度</param>
        /// <param name="baseMapHeight">底图高度</param>
        /// <param name="WatermarkWidth">水印宽度</param>
        /// <param name="WatermarkHeight">水印高度</param>
        /// <returns></returns>
        private static Rectangle GetCenterRectangle(int baseMapWidth, int baseMapHeight, int WatermarkWidth, int WatermarkHeight)
        {
            int markx = (baseMapWidth - WatermarkWidth) / 2;
            int marky = (baseMapHeight - WatermarkHeight) / 2;
            return new Rectangle(markx, marky, WatermarkWidth, WatermarkHeight);
        }
    }
}
