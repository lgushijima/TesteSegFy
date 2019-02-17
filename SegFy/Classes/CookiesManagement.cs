using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace SegFy.Classes
{
    public class CookiesManagement
    {
        public static void Extend(string name)
        {
            var ssn = Get(name);
            Save(ssn, name);
        }

        public static void Close(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddMonths(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static void Save(object usuario, string name)
        {
            Stream myStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(myStream, usuario);
            myStream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[myStream.Length];
            myStream.Read(buffer, 0, (int)myStream.Length);

            byte[] bufferCompressed = Crypto.Compress(buffer);

            HttpCookie cookie = new HttpCookie(name)
            {
                Expires = DateTime.Now.AddMinutes(90),
                Value = Convert.ToBase64String(bufferCompressed)
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static object Get(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie != null)
            {
                byte[] buffer = Convert.FromBase64String(cookie.Value);
                byte[] bufferDecompressed = Crypto.Decompress(buffer);

                Stream myStream = new MemoryStream(bufferDecompressed);

                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(myStream);
                }
                finally
                {
                    myStream.Close();
                }
            }
            return null;
        }
    }
}