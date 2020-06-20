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

namespace SalesAPI.Controllers.Tests
{
    [TestClass()]
    public class SalesControllerTests
    {        
        private SalesController controller;
        private ISalesRepository _repository;

        public SalesControllerTests(ISalesRepository repository)
        {
            _repository = repository;
            controller = new SalesController(_repository);
          
        }

        [TestMethod()]
        public void GetTest()
        {  
            var response = controller.Get();
            Assert.IsNotNull(response);
          
        }

        public void GetTestByID_Success()
        {
            var mockRepo = new Mock<ISalesRepository>();
            mockRepo.Setup(x => x.GetByID(2)).Returns(new Sale { invoiceID = 2, productID = 02, productName = "bbb", productQuantity = 8, totalPrice = 80 });

            var response = controller.Get(2);            
            Assert.IsNotNull(response);
        }

        private List<Sale> GetTestSale()
        {
            var testSales = new List<Sale>();
            testSales.Add(new Sale { invoiceID = 1, productID = 01, productName = "aaa", productQuantity = 5, totalPrice = 50 });
            testSales.Add(new Sale { invoiceID = 2, productID = 02, productName = "bbb", productQuantity = 8, totalPrice = 80 });
            return testSales;
        }
    }
}