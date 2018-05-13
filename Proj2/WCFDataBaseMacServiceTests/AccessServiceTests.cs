using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFDataBaseMacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTemplates;
using Moq;
using System.Data.Entity;
using DataTemplates.DisplayData;

namespace WCFDataBaseMacService.Tests
{


    [TestClass()]
    public class AccessServiceTests
    {
        /// <summary>
        /// Helps mock the ctxcontext DBSet when using linq
        /// </summary>
        private Mock<DbSet<TEntity>> MockDbSet<TEntity>(List<TEntity> list) where TEntity : class
        {
            var listQuery = list.AsQueryable();
            var mock = new Mock<DbSet<TEntity>>();

            mock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(listQuery.Provider);
            mock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(listQuery.Expression);
            mock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(listQuery.ElementType);
            mock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(listQuery.GetEnumerator());

            return mock;
        }

        [TestMethod()]
        public void LoginTest()
        {
            //Arrange
            string login = "Adam2";
            string haslo = "1234";
            string salt = "salt";

            var konto1 = new DBKonto() { Login = "Adam", Haslo = "12345", Salt = "salt" };
            var konto2 = new DBKonto() { Login = login, Haslo = haslo, Salt = salt };
            var kontos = new List<DBKonto>() { konto1, konto2 };

            var kontoMock = MockDbSet<DBKonto>(kontos);

            var ctxMock = new Mock<DataBaseModel>();
            ctxMock.Setup(m => m.Konto).Returns(kontoMock.Object);

            var md5Mock = new Mock<MD5Hash>();
            md5Mock.Setup(m => m.VerifyMd5Hash(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>())).Returns(false);
            md5Mock.Setup(m => m.VerifyMd5Hash(It.IsAny<String>(), It.IsAny<String>(), haslo)).Returns(true);

            var accesService = new AccessService(ctxMock.Object, md5Mock.Object);

            //Act
            bool success = accesService.Login(login, haslo);
            bool fail = accesService.Login("Adam3", "12345");
            bool fail2 = accesService.Login("Adam", "123456");

            //Assert
            Assert.IsTrue(success);
            Assert.IsFalse(fail);
            Assert.IsFalse(fail2);
        }

        [TestMethod()]
        public void GetKlientyTest()
        {
            //Arrange
            string login = "Adam2";
            string haslo = "1234";
            string salt = "salt";
            int clear = 4;

            var konto1 = new DBKonto() { Login = "Adam", Haslo = "12345", Salt = "salt" };
            var konto2 = new DBKonto() { Login = login, Haslo = haslo, Salt = salt ,Clear = clear};
            var kontos = new List<DBKonto>() { konto1, konto2 };

            var kontoMock = MockDbSet<DBKonto>(kontos);

            var klient1 = new DBKlient() { Imie = "Adam", Nazwisko = "NazwAdam", Adres = "AdresAdam" };
            var klient2 = new DBKlient() { Imie = "Adam", Nazwisko = "NazwAdam", Adres = "AdresAdam", Class = clear+1 };
            var klientos = new List<DBKlient>() { klient1, klient2 };

            var klientMock = MockDbSet<DBKlient>(klientos);

            var ctxMock = new Mock<DataBaseModel>();
            ctxMock.Setup(m => m.Konto).Returns(kontoMock.Object);
            ctxMock.Setup(m => m.Klient).Returns(klientMock.Object);

            var md5Mock = new Mock<MD5Hash>();
            md5Mock.Setup(m => m.VerifyMd5Hash(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>())).Returns(false);
            md5Mock.Setup(m => m.VerifyMd5Hash(It.IsAny<String>(), It.IsAny<String>(), haslo)).Returns(true);

            var accesService = new AccessService(ctxMock.Object, md5Mock.Object);
            accesService.Login(login, haslo);

            //Act
            List<Klient> result = accesService.GetKlienty();

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Adam", result[0].Imie);
            Assert.AreEqual("NazwAdam", result[0].Nazwisko);
            Assert.AreEqual("AdresAdam", result[0].Adres);
        }

        [TestMethod()]
        public void GetKontaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPracownicyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetProdkutyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetZamowieniaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetZam_prodyTest()
        {
            Assert.Fail();
        }
    }
}