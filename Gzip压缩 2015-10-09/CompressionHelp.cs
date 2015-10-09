using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace Gzip压缩_2015_10_09
{
    /// <summary>
    /// json格式的压缩和解压缩
    /// 作者：hjj
    /// 时间:2015-6-5
    /// </summary>
    public static class CompressionHelp
    {
        #region 压缩+Compress
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] input)
        {
            byte[] output;
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gs = new GZipStream(ms, CompressionMode.Compress))
                {
                    gs.Write(input, 0, input.Length);
                    gs.Close();
                    output = ms.ToArray();
                }
                ms.Close();
            }
            return output;
        } 
        #endregion

        #region 解压缩+Decompress
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] input)
        {
            List<byte> output = new List<byte>();
            using (MemoryStream ms = new MemoryStream(input))
            {
                using (GZipStream gs = new GZipStream(ms, CompressionMode.Decompress))
                {
                    int readByte = gs.ReadByte();
                    while (readByte != -1)
                    {
                        output.Add((byte)readByte);
                        readByte = gs.ReadByte();
                    }
                    gs.Close();
                }
                ms.Close();
            }
            return output.ToArray();
        } 
        #endregion

        #region 压缩2+DeflateByte
        /// <summary>
        /// 压缩2
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] DeflateByte(byte[] str)
        {
            if (str == null)
            {
                return null;
            }
            //这里要添加库Ionic
            using (var output = new MemoryStream())
            {
                using (
                  var compressor = new Ionic.Zlib.DeflateStream(
                  output, Ionic.Zlib.CompressionMode.Compress,
                  Ionic.Zlib.CompressionLevel.BestSpeed))
                {
                    compressor.Write(str, 0, str.Length);
                }

                return output.ToArray();
            }

            //当然如果使用GZIP压缩的话，只需要将
            //new Ionic.Zlib.DeflateStream( 改为
            //new Ionic.Zlib.GZipStream(，然后
            //actContext.Response.Content.Headers.Add("Content-encoding", "deflate");改为
            //actContext.Response.Content.Headers.Add("Content-encoding", "gzip");
            //就可以了，经本人测试，
            //Deflate压缩要比GZIP压缩后的代码要小，所以推荐使用Deflate压缩
        } 
        #endregion
    }

}