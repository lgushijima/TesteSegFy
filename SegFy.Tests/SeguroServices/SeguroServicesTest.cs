
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SegFy.Business;
using SegFy.Business.Data;
using SegFy.Business.Models;
namespace SegFy.Tests.SeguroServices
{
    [TestClass]
    public class SeguroServicesTest
    {
        private List<Seguro> _dataSource = new List<Seguro>() {
                new Seguro(){ ID=1, CPFCNPJ="12312312312", IDTipo=1, Placa="ABC1234"},
                new Seguro(){ ID=2, CPFCNPJ="11122233344", IDTipo=2, Endereco="Rua João da Silva, 50"},
                new Seguro(){ ID=3, CPFCNPJ="00011100011", IDTipo=3, CPF="12312312312"}
            };

        [TestMethod]
        public void Seguro_CriarNovo()
        {
            var data = _dataSource.AsQueryable();

            var mock = new Mock<DbSet<Seguro>>();
            mock.As<IQueryable<Seguro>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<Seguro>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<Seguro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<Seguro>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mock.Object.AddRange(data);
            var mockContext = new Mock<DBContext>();
            mockContext.Setup(m => m.Seguro).Returns(mock.Object);

            var service = new SeguroService(mockContext.Object);
            service.Insert(new SeguroModel() { IDTipo= 4, CPFCNPJ= "123123123-12", Placa="AYB5757"});

            mock.Verify(m => m.Add(It.IsAny<Seguro>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Seguro_GetAll()
        {
            var data = _dataSource.AsQueryable();

            var mock = new Mock<DbSet<Seguro>>();
            mock.As<IQueryable<Seguro>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<Seguro>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<Seguro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<Seguro>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mock.Object.AddRange(data);
            var mockContext = new Mock<DBContext>();
            mockContext.Setup(m => m.Seguro).Returns(mock.Object);

            var service = new SeguroService(mockContext.Object);
            var result = service.GetAll(new SeguroModel() { IDTipo = 1 });

            Assert.AreEqual(_dataSource.Where(d => d.IDTipo == 1).Count(), result.Count());
        }

        [TestMethod]
        public void Seguro_Delete()
        {
            var data = _dataSource.AsQueryable();

            var mock = new Mock<DbSet<Seguro>>();
            mock.As<IQueryable<Seguro>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<Seguro>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<Seguro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<Seguro>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DBContext>();
            mockContext.Setup(m => m.Seguro).Returns(mock.Object);

            var service = new SeguroService(mockContext.Object);
            var result = service.Delete(1);
            Assert.AreEqual(true, result);
        }
    }
}
