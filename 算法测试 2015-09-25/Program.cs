using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 算法测试_2015_09_25
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MSDN 下面的代码示例创建一个 TripleDESCryptoServiceProvider 对象并使用该对象加密和解密文件中的数据。
            try
            {
                // Create a new TripleDESCryptoServiceProvider object
                // to generate a key and initialization vector (IV).
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();

                // Create a string to encrypt.
                string sData = "Here is some data to encrypt.";
                string FileName = "CText.txt";

                // Encrypt text to a file using the file name, key, and IV.
                EncryptTextToFile(sData, FileName, tDESalg.Key, tDESalg.IV);

                // Decrypt the text from a file using the file name, key, and IV.
                string Final = DecryptTextFromFile(FileName, tDESalg.Key, tDESalg.IV);

                // Display the decrypted string to the console.
                Console.WriteLine(Final);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
            #endregion

            #region MSDN 下面的代码示例创建一个 TripleDESCryptoServiceProvider 对象并使用该对象加密和解密内存中的数据。
            try
            {
                // Create a new TripleDESCryptoServiceProvider object
                // to generate a key and initialization vector (IV).
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();

                // Create a string to encrypt.
                string sData = "Here is some data to encrypt.";

                // Encrypt the string to an in-memory buffer.
                byte[] Data = EncryptTextToMemory(sData, tDESalg.Key, tDESalg.IV);

                // Decrypt the buffer back to a string.
                string Final = DecryptTextFromMemory(Data, tDESalg.Key, tDESalg.IV);

                // Display the decrypted string to the console.
                Console.WriteLine(Final);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
            #endregion

            //string tx = btnI2Gen("sidfaionfoa");
            #region link 3DES加解密 
            Console.WriteLine("Encrypt String...");
            txtKey = "tkGGRmBErvc=";      
            btnKeyGen();
            Console.WriteLine("Encrypt Key :{0}", txtKey);
            txtIV = "Kl7ZgtM1dvQ=";
            btnIVGen();
            Console.WriteLine("Encrypt IV :{0}", txtIV);
            Console.WriteLine();
            string txtEncrypted = EncryptString("1111");
            Console.WriteLine("Encrypt String : {0}", txtEncrypted);
            string txtOriginal = DecryptString(txtEncrypted);
            Console.WriteLine("Decrypt String : {0}", txtOriginal); 
            #endregion

            Console.ReadLine();
        }
        

         public static void EncryptTextToFile(String Data, String FileName, byte[] Key, byte[] IV)
         {
             try
             {
                 // Create or open the specified file.
                 FileStream fStream = File.Open(FileName,FileMode.OpenOrCreate);

                 // Create a CryptoStream using the FileStream 
                 // and the passed key and initialization vector (IV).
                 CryptoStream cStream = new CryptoStream(fStream, 
                     new TripleDESCryptoServiceProvider().CreateEncryptor(Key,IV), 
                     CryptoStreamMode.Write); 

                 // Create a StreamWriter using the CryptoStream.
                 StreamWriter sWriter = new StreamWriter(cStream);

                 // Write the data to the stream 
                 // to encrypt it.
                 sWriter.WriteLine(Data);

                 // Close the streams and
                 // close the file.
                 sWriter.Close();
                 cStream.Close();
                 fStream.Close();
             }
             catch(CryptographicException e)
             {
                 Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
             }
             catch(UnauthorizedAccessException  e)
             {
                 Console.WriteLine("A file access error occurred: {0}", e.Message);
             }

         }

         public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV)
         {
             try
             {
                 // Create or open the specified file. 
                 FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                 // Create a CryptoStream using the FileStream 
                 // and the passed key and initialization vector (IV).
                 CryptoStream cStream = new CryptoStream(fStream, 
                     new TripleDESCryptoServiceProvider().CreateDecryptor(Key,IV), 
                     CryptoStreamMode.Read); 

                 // Create a StreamReader using the CryptoStream.
                 StreamReader sReader = new StreamReader(cStream);

                 // Read the data from the stream 
                 // to decrypt it.
                 string val = sReader.ReadLine();

                 // Close the streams and
                 // close the file.
                 sReader.Close();
                 cStream.Close();
                 fStream.Close();

                 // Return the string. 
                 return val;
             }
             catch(CryptographicException e)
             {
                 Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                 return null;
             }
             catch(UnauthorizedAccessException  e)
             {
                 Console.WriteLine("A file access error occurred: {0}", e.Message);
                 return null;
             }
         }

         public static byte[] EncryptTextToMemory(string Data, byte[] Key, byte[] IV)
         {
             try
             {
                 // Create a MemoryStream.
                 MemoryStream mStream = new MemoryStream();

                 // Create a CryptoStream using the MemoryStream 
                 // and the passed key and initialization vector (IV).
                 CryptoStream cStream = new CryptoStream(mStream,
                     new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV),
                     CryptoStreamMode.Write);

                 // Convert the passed string to a byte array.
                 byte[] toEncrypt = new ASCIIEncoding().GetBytes(Data);

                 // Write the byte array to the crypto stream and flush it.
                 cStream.Write(toEncrypt, 0, toEncrypt.Length);
                 cStream.FlushFinalBlock();

                 // Get an array of bytes from the 
                 // MemoryStream that holds the 
                 // encrypted data.
                 byte[] ret = mStream.ToArray();

                 // Close the streams.
                 cStream.Close();
                 mStream.Close();

                 // Return the encrypted buffer.
                 return ret;
             }
             catch (CryptographicException e)
             {
                 Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                 return null;
             }

         }

         public static string DecryptTextFromMemory(byte[] Data, byte[] Key, byte[] IV)
         {
             try
             {
                 // Create a new MemoryStream using the passed 
                 // array of encrypted data.
                 MemoryStream msDecrypt = new MemoryStream(Data);

                 // Create a CryptoStream using the MemoryStream 
                 // and the passed key and initialization vector (IV).
                 CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                     new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV),
                     CryptoStreamMode.Read);

                 // Create buffer to hold the decrypted data.
                 byte[] fromEncrypt = new byte[Data.Length];

                 // Read the decrypted data out of the crypto stream
                 // and place it into the temporary buffer.
                 csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                 //Convert the buffer into a string and return it.
                 return new ASCIIEncoding().GetString(fromEncrypt);
             }
             catch (CryptographicException e)
             {
                 Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                 return null;
             }
         }

         #region 3DES加密 + link：http://www.cnblogs.com/hcbin/archive/2010/08/30/1812353.html
         private static SymmetricAlgorithm mCSP;
         private static string txtKey;       //密钥，必须12位
         private static string txtIV;        //向量，必须是12个字符
         private static void btnKeyGen()
         {
             mCSP = SetEnc();
             byte[] byt2 = Convert.FromBase64String(txtKey);
             mCSP.Key = byt2;
         }
         private static void btnIVGen()
         {
             byte[] byt2 = Convert.FromBase64String(txtIV);
             mCSP.IV = byt2;
         }
         //private static string btnI2Gen(string txt)
         //{
         //    byte[] byteArray =System.Text.Encoding.Default.GetBytes(txt);
         //    return Convert.ToBase64String(byteArray);
         //}
         private static string EncryptString(string Value)
         {
             ICryptoTransform ct;
             MemoryStream ms;
             CryptoStream cs;
             byte[] byt;
             ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
             byt = Encoding.UTF8.GetBytes(Value);
             ms = new MemoryStream();
             cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
             cs.Write(byt, 0, byt.Length);
             cs.FlushFinalBlock();
             cs.Close();
             return Convert.ToBase64String(ms.ToArray());
         }
         private static string DecryptString(string Value)
         {
             ICryptoTransform ct;
             MemoryStream ms;
             CryptoStream cs;
             byte[] byt;
             ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
             byt = Convert.FromBase64String(Value);
             ms = new MemoryStream();
             cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
             cs.Write(byt, 0, byt.Length);
             cs.FlushFinalBlock();
             cs.Close();
             return Encoding.UTF8.GetString(ms.ToArray());
         }
         private static SymmetricAlgorithm SetEnc()
         {
             return new DESCryptoServiceProvider();
         }
         #endregion
    }
}
