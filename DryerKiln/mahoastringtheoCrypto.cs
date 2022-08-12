using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Xml;

namespace Tdin20.tdinCode
{
    class mahoastringtheoCrypto
    {



        public mahoastringtheoCrypto()
        {
        }
        private byte[] key = {};
        private byte[] IV = {18, 52, 86, 120, 144, 171, 205, 239};

        private string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {

            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(Left(sEncryptionKey, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private string Encrypt(string stringToEncrypt, string SEncryptionKey)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private string Left(string MyString,int length)
        {
            string tmpstr = MyString.Substring(0, length);
            return tmpstr;
        }

        public string EncryptQueryString(string strQueryString)
        {
            return Encrypt(strQueryString,"!#$a54?3ju223td123bc");
        }

        public string DecryptQueryString(string strQueryString)
        {
            return Decrypt(strQueryString, "!#$a54?3ju223td123bc");
        }
    }
}
