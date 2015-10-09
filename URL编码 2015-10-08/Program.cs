using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URL编码_2015_10_08
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Web.HttpUtility：提供用于在处理 Web 请求时编码和解码 URL 的方法。 此类不能被继承。
            string strParameter = "a=3";        //参数。

            string result1 = System.Web.HttpUtility.UrlEncode(strParameter);                    //result1:a%3d3
            string result2 = System.Web.HttpUtility.UrlEncode(strParameter, Encoding.UTF8);      //result2:a%3d3
            //总结对比上面两个结果知道System.Web.HttpUtility.UrlEncode方法默认采用UTF8编码格式

            //------------------------------------------------------------------------------------------------------------------------------------------------------------

            //对于参数编码使用System.Web.HttpUtility.UrlEncode够了，但是对于URL编码还有其他的类
            //参考 http://www.cnblogs.com/artwl/archive/2012/03/07/2382848.html 这篇文章，知道C#中编码主要方法：HttpUtility.UrlEncode、Server.UrlEncode、Uri.EscapeUriString、Uri.EscapeDataString。
            //下面我们做一些测试。

            string strUrl = "http://www.cnblogs.com/a file with spaces.html?a=1&b=博客园#abc";    //url

            string res1 = System.Web.HttpUtility.UrlEncode(strUrl);                 //res1:http%3a%2f%2fwww.cnblogs.com%2fa+file+with+spaces.html%3fa%3d1%26b%3d%e5%8d%9a%e5%ae%a2%e5%9b%ad%23abc
            //Server.UrlEncode在System.Web.HttpContext命名空间下,Server.UrlEncode是使用系统预设格式编码
            string res2 = System.Uri.EscapeUriString(strUrl);                       //res2:http://www.cnblogs.com/a%20file%20with%20spaces.html?a=1&b=%E5%8D%9A%E5%AE%A2%E5%9B%AD#abc
            string res3 = System.Uri.EscapeDataString(strUrl);                      //res3:http%3A%2F%2Fwww.cnblogs.com%2Fa%20file%20with%20spaces.html%3Fa%3D1%26b%3D%E5%8D%9A%E5%AE%A2%E5%9B%AD%23abc
            //总结：在C#中推荐的做法是用Uri.EscapeUriString对URI的网址部分编码.

        }
    }
}
