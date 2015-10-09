using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gzip压缩_2015_10_09
{
    class Program
    {
        static void Main(string[] args)
        {
            //压缩分为三种：1.字节数组 2.对文件的压缩 3.对字符串的压缩
            //下面掩饰的是加解密字节数组

            byte[] cbytes = null;
            //压缩
            using (MemoryStream cms = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(cms, System.IO.Compression.CompressionMode.Compress))
                {
                    //将数据写入基础流，同时会被压缩
                    byte[] bytes = Encoding.UTF8.GetBytes("解压缩测试");
                    gzip.Write(bytes, 0, bytes.Length);
                }
                cbytes = cms.ToArray();
            }
            //解压
            using (MemoryStream dms = new MemoryStream())
            {
                using (MemoryStream cms = new MemoryStream(cbytes))
                {
                    using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(cms, System.IO.Compression.CompressionMode.Decompress))
                    {
                        byte[] bytes = new byte[1024];
                        int len = 0;
                        //读取压缩流，同时会被解压
                        while ((len = gzip.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            dms.Write(bytes, 0, len);
                        }
                    }
                }
                Console.WriteLine(Encoding.UTF8.GetString(dms.ToArray()));      //result:"解压缩测试"
            }

            //————————————————————————————————————————————
            //下面示例来自：http://www.cnblogs.com/yank/p/Compress.html


            TestGZipCompressFile();

            string str = "abssssdddssssssssss11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";
            string orgStr = str;
            string result = "";

            //将传入的字符串直接进行压缩，解压缩，会出现乱码。
            //先转码，然后再压缩。解压缩：先解压缩，再反转码
            Console.WriteLine("源字符串为：{0}", str);
            result = GZipCompress.Compress(str);
            Console.WriteLine("压缩后为：{0}", result);

            Console.Write("压缩前：{0},压缩后：{1}", str.Length, result.Length);

            Console.WriteLine("开始解压...");
            Console.WriteLine("解压后：{0}", result);
            result = GZipCompress.Decompress(result);
            Console.WriteLine("解压后与源字符串对比，是否相等：{0}", result == orgStr);


            Console.WriteLine("源字符串为：{0}", str);
            result = ZipComporessor.Compress(str);
            Console.WriteLine("压缩后为：{0}", result);

            Console.Write("压缩前：{0},压缩后：{1}", str.Length, result.Length);

            Console.WriteLine("开始解压...");
            Console.WriteLine("解压后：{0}", result);
            result = ZipComporessor.Decompress(result);
            Console.WriteLine("解压后与源字符串对比，是否相等：{0}", result == orgStr);

            Console.WriteLine("输入任意键，退出！");
            Console.ReadKey();
        }


        /// <summary>
        /// 测试GZipCompress压缩文件
        /// </summary>
        public static void TestGZipCompressFile()
        {
            string filePath = @"c:\test.txt";

            FileInfo fileToCompress = new FileInfo(filePath);
            GZipCompress.Compress(fileToCompress);

            FileInfo fileToDecompress = new FileInfo(@"c:\test.txt.gz");
            GZipCompress.Decompress(fileToDecompress);
        }
    }
}
