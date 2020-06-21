using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using SalesAPI.Model;
using SalesAPI.Data;
using SalesAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Web.Http.Results;
using System.Linq;

namespace SalesAPI.Controllers.Tests
{
    [TestClass()]
    public class SalesControllerTests
    {        
        private SalesController controller;
        private Mock<ISalesRepository> mockRepo;       

        
        [TestInitialize]
        public void Initialize()
        {
            mockRepo = new Mock<ISalesRepository>();            
        }

        [TestMethod()]
        public void GetTest_Success()
        {
            mockRepo.Setup(x => x.GetSales()).Returns(GetTestSale());
            controller = new SalesController(mockRepo.Object);

            var response = controller.Get();
            Assert.IsNotNull(response);

        }
        [TestMethod()]
        public void GetTestByID_Success()
        {
            mockRepo.Setup(x => x.GetByID(2)).Returns(new Sale { invoiceID = 2, productID = 02, productName = "bbb", productQuantity = 8, totalPrice = 80 });
            controller = new SalesController(mockRepo.Object);
            
            var response = controller.Get(2);
            Assert.AreEqual(response.productName, "bbb");
        }

        private IList<Sale> GetTestSale()
        {
            var testSales = new List<Sale>();
            testSales.Add(new Sale { invoiceID = 1, productID = 01, productName = "aaa", productQuantity = 5, totalPrice = 50 });
            testSales.Add(new Sale { invoiceID = 2, productID = 02, productName = "bbb", productQuantity = 8, totalPrice = 80 });
            return testSales;
        }
    }
}