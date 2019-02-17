using SegFy.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegFy.Business.Models
{
    [Serializable]
    public class UsuarioModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public int ID { get; set; }
    }
}
