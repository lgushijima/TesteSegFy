using SegFy.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SegFy.Business.Models
{
    [Serializable]
    public class SeguroModel
    {
        public int ID { get; set; }
        public string CPFCNPJ { get; set; }
        public int IDTipo { get; set; }
        public string Placa { get; set; }
        public string Endereco { get; set; }
        public string CPFSegurado { get; set; }

    }
}
