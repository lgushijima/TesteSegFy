using SegFy.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegFy.Business.Models
{
    public class UsuarioService : IBusiness<UsuarioModel>
    {
        private DBContext _dbContext { get; set; }
        public UsuarioService(DBContext context)
        {
            _dbContext = context;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel ValidateLogin(UsuarioModel model)
        {
            var user = (from u in _dbContext.Usuario
                        where
                            u.Login == model.Login &&
                            u.Senha == model.Senha
                        select new UsuarioModel {
                            ID= u.ID,
                            Login = u.Login
                        }).FirstOrDefault();

            return user;
        }

        public IEnumerable<UsuarioModel> GetAll(UsuarioModel model)
        {
            throw new NotImplementedException();
        }

        public void Insert(UsuarioModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(UsuarioModel model)
        {
            throw new NotImplementedException();
        }
    }
}
