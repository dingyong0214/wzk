// ==============================================
// Author:陈根发
// Create date: 2013-5-24
// Description: 加密解密相关
// Update:
// ===============================================
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace WZK.Common
{
    public class Encrypt
    {
        private static string sKey = "cayicaAAA111aaa";
        private Encrypt() { }

        #region MD5加密
        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="encryptString">加密字符串.</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-07-09 15:31:19 </remarks>
        public static string MD5(string encryptString)
        {
            byte[] bytes = Encoding.Default.GetBytes(encryptString);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string result = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                result = result + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return result;
        }
        #endregion

        #region SHA1加密
        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="encryptString">加密字符串.</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-08-12</remarks>
        public static string SHA1(string encryptString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            bytes = new SHA1CryptoServiceProvider().ComputeHash(bytes);
            string result = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                result = result + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return result;
        }
        #endregion

        #region 不需要密钥的加密
        /// <summary>
        /// 不需要密钥的加密
        /// </summary>
        /// <param name="Text">需要加密的字符串</param>
        /// <returns></returns>
        public static string EncryptDES(string Text)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        #endregion

        #region 不需要密钥的解密
        /// <summary>
        /// 不需要密钥的解密
        /// </summary>
        /// <param name="Text">需要解密的字符串</param>
        /// <returns></returns>
        public static string DecryptDES(string Text)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

        #region DES加密字符串
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(encryptString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(encryptKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(encryptKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        #endregion

        #region DES解密字符串
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                int len;
                len = decryptString.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(decryptString.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(decryptKey, "md5").Substring(0, 8));
                des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(decryptKey, "md5").Substring(0, 8));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }
        #endregion

        #region 根据密钥加密
        /// <summary>
        /// 根据密钥加密
        /// </summary>
        /// <param name="encryptString">需要加密的字符串</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static string EncryptString(string encryptString, string encryptKey)
        {
            try
            {
                string sIV = "cayicaTask";
                byte[] data = Encoding.UTF8.GetBytes(encryptString);

                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();

                DES.Key = ASCIIEncoding.ASCII.GetBytes(encryptKey);

                DES.IV = ASCIIEncoding.ASCII.GetBytes(sIV);

                ICryptoTransform desencrypt = DES.CreateEncryptor();

                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);

                return BitConverter.ToString(result);
            }
            catch
            {
                return "加密出错！";
            }
        }
        #endregion

        #region 根据密钥解密
        /// <summary>
        /// 根据密钥解密
        /// </summary>
        /// <param name="decryptString">需要解密的字符串</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns></returns>
        public static string DecryptString(string decryptString, string decryptKey)
        {
            try
            {
                string sIV = "cayicaTask";
                string[] sInput = decryptString.Split("-".ToCharArray());

                byte[] data = new byte[sInput.Length];

                for (int i = 0; i < sInput.Length; i++)
                {
                    data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
                }

                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(decryptKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sIV);
                ICryptoTransform desencrypt = DES.CreateDecryptor();

                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);

                return Encoding.UTF8.GetString(result);
            }
            catch
            {
                return "解密出错！";
            }

        }
        #endregion 
    }
}