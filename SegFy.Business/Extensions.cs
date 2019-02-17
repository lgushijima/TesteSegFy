using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegFy.Business
{
    public static class Extensions
    {
        public static string Remove(this string valor, string chars)
        {
            if (String.IsNullOrEmpty(valor))
                return valor;
            else
            {
                for (var i = 0; i < chars.Length; i++)
                {
                    valor = valor.Replace(chars[i].ToString(), "");
                }
                return valor;
            }
        }
    }
}
