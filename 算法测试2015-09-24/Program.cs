using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法测试2015_09_24
{
    class Program
    {
        static void Main(string[] args)
        {
            Security s = new Security();
            //s.InitialReg("/rsaPath");           // 初始化注册表，程序运行时调用，在调用之前更新公钥xml
            s.RSAKey("../PrivateKey.xml", "../PublicKey.xml");    //生成rsa公私钥  C:\Users\Administrator\Desktop\webapi相关测试\算法测试2015-09-24\bin 下可以找到
            string PrivateKey = s.ReadPrivateKey("../PrivateKey.xml");
            string PublicKey = s.ReadPrivateKey("../PublicKey.xml");
          
          
            string HDInfo = s.GetHardID();      //获取用户硬盘号
            string HDInfoRsaStr = s.RSAEncrypt(PublicKey, HDInfo);   //服务端对用户硬盘号进行RSA加密
            string HDInfoDRsaStr = s.RSADecrypt(PrivateKey, HDInfoRsaStr);  //客户端用公钥解密
            string HDInfoMD5Str = s.GetHash(HDInfoDRsaStr);      //客户端对解密后的数据用MD5加密

            //string strContent = "我是地球人，我是中国人，我是浙江人，我是温州人！";     //发送消息
            //string strContent = "Hello!";
            //string Md5Str = s.GetHash(strContent);      //对字符进行MD5加密
            //string RsaStr = s.RSAEncrypt(PublicKey, Md5Str);       // RSA加密
            //string DRsaStr = s.RSADecrypt(PrivateKey,RsaStr);      // RSA解密


            #region 用户签名验证
            string usernameAndPwd = "cby" + "123456";
            //string code = "123456";         //注册码
            string usernameAndPwdMd5Str = s.GetHash(usernameAndPwd);      //客户端对用户名进行MD5加密
            string codeMd5Str = s.GetHash(usernameAndPwd);      //对验证码进行MD5加密
            string SignatureStr = s.SignatureFormatter(PrivateKey, usernameAndPwdMd5Str);      //对MD5加密后的密文进行签名
            bool isRihtSignature = s.SignatureDeformatter(PublicKey, usernameAndPwdMd5Str, SignatureStr);      //签名验证 
            #endregion



            #region 使用System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile方法进行MD5和SHA1的加密
            //string temStr1 = "用我来计算SHA1值吧!";
            //string temStr2 = "用你来计算SHA1值吧!";
           
            //string SHA1Str1 = s.EncryptByHashPasswordForStoringInConfigFileMethod(temStr1, "SHA1");
            //string SHA1Str2 = s.EncryptByHashPasswordForStoringInConfigFileMethod(temStr2, "SHA1");
            //Console.WriteLine("SHA1计算所得值1：" + SHA1Str1 + "\r\n" + "SHA1计算所得值2：" + SHA1Str2);

            //string MD5Str1 = s.EncryptByHashPasswordForStoringInConfigFileMethod(temStr1, "MD5");
            //string MD5Str2 = s.EncryptByHashPasswordForStoringInConfigFileMethod(temStr2, "MD5");
            //Console.WriteLine("MD5计算所得值1：" + MD5Str1 + "\r\n" + "MD5计算所得值2：" + MD5Str2); 
            #endregion

            //Console.ReadKey();
        }

    }
}
