using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO.Compression;
using System.IO;

namespace SegFy
{
    public class Crypto
    {
        private TripleDESCryptoServiceProvider TripleDES = new TripleDESCryptoServiceProvider();
        private MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
        private string key = "S3g#Fy";

        public Crypto()
        {
        }


        public string Encrypt(string stringToEncrypt)
        {
            try
            {
                TripleDES.Key = this.MD5Hash(key);
                TripleDES.Mode = CipherMode.ECB;
                byte[] Buffer = ASCIIEncoding.UTF8.GetBytes(stringToEncrypt);
                return ByteArrayToHexString(TripleDES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Decrypt(string encryptedString)
        {
            try
            {
                var keycreated = this.MD5Hash(key);
                TripleDES.Key = keycreated;
                TripleDES.Mode = CipherMode.ECB;
                byte[] Buffer = HexStringToByteArray(encryptedString);
                return ASCIIEncoding.UTF8.GetString(TripleDES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private byte[] MD5Hash(string value)
        {
            byte[] byteArray = ASCIIEncoding.UTF8.GetBytes(value);
            return MD5.ComputeHash(byteArray);
        }

        public string ByteArrayToHexString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public byte[] HexStringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

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

        public static byte[] Decompress(byte[] input)
        {
            var output = new List<byte>();
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
    }
}