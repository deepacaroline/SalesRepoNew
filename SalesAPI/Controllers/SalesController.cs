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
        
        public SalesController(ISalesRepository repository)
        {
            _repository = repository;            
        }

        // GET: api/<SalesController>
        [HttpGet]
        public IEnumerable<Sale> Get()
        {
            var result = _repository.GetSales();            
            return result;            
        }
        
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

        }       
    }
}
