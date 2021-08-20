﻿using System;

using System.Collections.Generic;
using System.Text;

using System.Security.Cryptography;
using System.IO;

namespace TpsViewZhongXunNameSpace.Utility
{
    public enum EncryptionKeyEnum
    {
        KeyA,
        KeyB
    }

    public class EncryptionHelper
    {
        string encryptionKeyA = "abbrawac";
        string encryptionKeyB = "abbwacfpd";
        string md5Begin = "m";
        string md5End = "h";
        string encryptionKey = string.Empty;

        public EncryptionHelper()
        {
            this.InitKey(EncryptionKeyEnum.KeyA);
        }

        public EncryptionHelper(EncryptionKeyEnum key)
        {
            this.InitKey(key);
        }

        private void InitKey(EncryptionKeyEnum key)
        {
            switch (key)
            {
                case EncryptionKeyEnum.KeyA:
                    encryptionKey = encryptionKeyA;
                    break;
                case EncryptionKeyEnum.KeyB:
                    encryptionKey = encryptionKeyB;
                    break;
            }
        }

        public string EncryptString(string str)
        {
            return Encrypt(str, encryptionKey);
        }

        public string DecryptString(string str)
        {
            return Decrypt(str, encryptionKey);
        }

        public string GetMD5String(string str)
        {
            str = string.Concat(md5Begin, str, md5End);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.Unicode.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string md5String = string.Empty;
            foreach (var b in targetData)
            {
                md5String += b.ToString("x2");
            }
            return md5String;
        }

        private string Encrypt(string str, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.Append(b.ToString("X2"));
                //ret.AppendFormat("{0:X2}", b);  
            }
            ret.ToString();
            return ret.ToString();
        }

        private string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray(), 0, Convert.ToInt32(ms.Length));
        }
    }
}
