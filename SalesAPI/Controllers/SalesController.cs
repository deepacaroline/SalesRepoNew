using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Net.Http.formatting
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SalesAPI.Data;
using SalesAPI.Model;
using SalesAPI.Repository;
//using System.Web.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        private ISalesRepository _repository;        
        //private IDistributedCache _distributedCache;

        //public SalesController(SalesDBContext salesDBContext, IDistributedCache distributedCache)
        public SalesController(ISalesRepository repository)
        {
            _repository = repository;
            //_salesDBContext = salesDBContext;
            //_distributedCache = distributedCache;
        }


        // GET: api/<SalesController>
        [HttpGet]
        public IEnumerable<Sale> Get()
        {
            var result = _repository.GetSales();
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return result;
            //return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
            //return _salesDBContext.Sales;
        }
        //public IActionResult Get()
        //{
        //    //return Ok(_salesDBContext.Sales);
        //    var cacheKey = "SalesValues";
        //    var jsonData = _distributedCache.GetString(cacheKey);
        //    var allsales = JsonConvert.DeserializeObject<List<Sale>>(jsonData);
        //    if (allsales != null)
        //    {
        //        return Ok(allsales);
        //    }
        //    else
        //    {
        //        //var saleItems = _salesDBContext.Sales;
        //        allsales = _salesDBContext.Sales.ToList();
        //        jsonData = JsonConvert.SerializeObject(allsales);
        //        _distributedCache.SetString(cacheKey, jsonData);
        //        return Ok(allsales);
                
        //    }


        //    //var saleItem = _salesDBContext.Sales;
        //    //return Ok(saleItem);
        //}

        // GET api/<SalesController>/5
        [HttpGet("{id}")]        
        public Sale Get(int id)
        {            
            var result = _repository.GetByID(id);            
            return result;
        }        

        // POST api/<SalesController>
        [HttpPost]
        public void Post([FromBody] Sale sale)
        {            
            _repository.AddSale(sale);
           
        }

        // PUT api/<SalesController>/5
        [HttpPut]
        public void Put([FromBody] Sale sale)
        { 
            _repository.EditSale(sale);
           
        }

        // DELETE api/<SalesController>/5
        [HttpDelete]
        public void Delete(int id)
        {
            _repository.RemoveSale(id);
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            //return response;
            //var saleItem = _salesDBContext.Sales.Find(id);
            //if (saleItem == null)
            //{
            //    return NotFound("Record not found with this ID !");
            //}
            //else
            //{
            //    _salesDBContext.Sales.Remove(saleItem);
            //    _salesDBContext.SaveChanges();
            //    return Ok("Record deleted !");
            //}

        }
        // api/Sales/RouteTestTotal/1
        //[HttpGet("[action]/{id}")]
        //public int RouteTestTotal(int id)
        //{
        //    var saleItem = _salesDBContext.Sales.Find(id);
        //    return saleItem.totalPrice;
        //}

        // api/Sales/SearchSales?prodName=Chalk
        //[HttpGet]
        //[Route("[action]")]
        //public IActionResult SearchSales(string prodName)
        //{
        //    var saleItem = _salesDBContext.Sales.Where(s => s.productName.StartsWith(prodName));
        //    return Ok(saleItem);
        //}
    }
}
