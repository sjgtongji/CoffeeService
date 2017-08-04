﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace XMS.Inner.Coffee.Business
{
    public static class IOHelper
    {
        /// <summary>
        /// 得到一个合理的文件名称
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        public static string GetValidFileName(string sFileName)
        {
            if (String.IsNullOrEmpty(sFileName)) return null;
            foreach (char lDisallowed in Path.GetInvalidFileNameChars())
            {
                sFileName = sFileName.Replace(lDisallowed.ToString(), "");
            }
            foreach (char lDisallowed in Path.GetInvalidPathChars())
            {
                sFileName = sFileName.Replace(lDisallowed.ToString(), "");
            }
            return sFileName;
        }

        /* - - - - - - - - - - - - - - - - - - - - - - - - 
         * Stream 和 byte[] 之间的转换
         * - - - - - - - - - - - - - - - - - - - - - - - */
        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }


        /* - - - - - - - - - - - - - - - - - - - - - - - - 
         * Stream 和 文件之间的转换
         * - - - - - - - - - - - - - - - - - - - - - - - */
        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}