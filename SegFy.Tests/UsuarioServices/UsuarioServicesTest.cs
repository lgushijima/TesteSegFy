
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SegFy.Business;
using SegFy.Business.Data;
using SegFy.Business.Models;

namespace SegFy.Tests.UsuarioServices
{
    [TestClass]
    public class UsuarioServicesTest
    {
        private List<Usuario> _dataSource = new List<Usuario>() {
                new Usuario(){ ID=1, Login="admin", Senha="123" },
                new Usuario(){ ID=2, Login="user", Senha="456" }
            };

        [TestMethod]
        public void Usuario_ValidarLogin()
        {
            var data = _dataSource.AsQueryable();

            var mock = new Mock<DbSet<Usuario>>();
            mock.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DBContext>();
            mockContext.Setup(m => m.Usuario).Returns(mock.Object);

            var service = new UsuarioService(mockContext.Object);
            var result = service.ValidateLogin(new UsuarioModel() { Login = "admin", Senha = "123" });

            var expected = new UsuarioModel() { ID = 1, Login = "admin", Senha = null };

            var js = new JavaScriptSerializer();
            Assert.AreEqual(js.Serialize(expected), js.Serialize(result));
        }
        
    }
}
