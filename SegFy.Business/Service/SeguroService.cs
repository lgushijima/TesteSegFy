using SegFy.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegFy.Business.Models
{
    public class SeguroService : IBusiness<SeguroModel>
    {
        private DBContext _dbContext { get; set; }
        public SeguroService(DBContext context)
        {
            _dbContext = context;
        }


        public bool Delete(int id)
        {
            var obj = (from u in _dbContext.Seguro
                       where
                            u.ID == id
                       select u).SingleOrDefault();

            if (obj != null)
            {
                _dbContext.Seguro.Remove(obj);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public SeguroModel Get(int id)
        {
            var obj = (from u in _dbContext.Seguro
                       where
                            u.ID == id
                       select new SeguroModel
                       {
                           ID = u.ID,
                           CPFCNPJ = u.CPFCNPJ,
                           IDTipo = u.IDTipo,
                           Placa = u.Placa,
                           Endereco = u.Endereco,
                           CPFSegurado = u.CPF
                       }).SingleOrDefault();

            if (obj != null)
                return obj;
            else
                throw new Exception("Nenhum registro encontrado com este código.");
        }

        public IEnumerable<SeguroModel> GetAll(SeguroModel model)
        {
            var obj = (from u in _dbContext.Seguro
                       where
                            (string.IsNullOrEmpty(model.CPFCNPJ) || u.CPFCNPJ.Contains(model.CPFCNPJ)) &&
                            (string.IsNullOrEmpty(model.Placa) || u.Placa.Contains(model.Placa)) &&
                            (model.IDTipo == 0 || u.IDTipo == model.IDTipo)
                       select new SeguroModel
                       {
                           ID = u.ID,
                           CPFCNPJ = u.CPFCNPJ,
                           IDTipo = u.IDTipo,
                           Placa = u.Placa,
                           Endereco = u.Endereco,
                           CPFSegurado = u.CPF
                       }).ToList();

            return obj;
        }

        public void Insert(SeguroModel model)
        {
            var obj = (from u in _dbContext.Seguro
                       where
                           u.ID == model.ID
                       select u).FirstOrDefault();

            if (obj == null)
            {
                Seguro seguro = new Seguro();
                seguro.IDTipo = model.IDTipo;
                seguro.CPFCNPJ = model.CPFCNPJ.Remove(".-/ ");
                seguro.CPF = model.CPFSegurado.Remove(".- ");
                seguro.Placa = model.Placa.Remove(" ");
                seguro.Endereco = model.Endereco;

                _dbContext.Seguro.Add(seguro);
                _dbContext.SaveChanges();
            }
            else
                throw new Exception("Já existe um registro com este código.");
        }


        public void Update(SeguroModel model)
        {
            var obj = (from u in _dbContext.Seguro
                       where
                           u.ID == model.ID
                       select u).FirstOrDefault();

            if (obj != null)
            {
                obj.IDTipo = model.IDTipo;
                obj.CPFCNPJ = model.CPFCNPJ.Remove(".-/ ");
                obj.CPF = model.CPFSegurado.Remove(".- ");
                obj.Placa = model.Placa.Remove(" ");
                obj.Endereco = model.Endereco;
                _dbContext.SaveChanges();
            }
            else
                throw new Exception("Nenhum registro encontrado com este código.");
        }
    }
}
