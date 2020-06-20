using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Data;
using SalesAPI.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace SalesAPI.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private SalesDBContext _salesDBContext;
        private IDistributedCache _distributedCache;
        public SalesRepository(SalesDBContext saleDBContext, IDistributedCache distributedCache)
        {
            _salesDBContext = saleDBContext;
            _distributedCache = distributedCache;
        }

        public void AddSale(Sale saleItem)
        {
            _salesDBContext.Sales.Add(saleItem);
            _salesDBContext.SaveChanges();
        }

        public void EditSale(Sale saleItem)
        {
            _salesDBContext.Entry(saleItem).State = EntityState.Modified;
            _salesDBContext.SaveChanges();
        }

        public Sale GetByID(int ID)
        {
            //var saleItem = _salesDBContext.Sales.AsNoTracking().Where(item => item.invoiceID == ID).FirstOrDefault();
            return _salesDBContext.Sales.Find(ID);
        }

        public IEnumerable<Sale> GetSales()
        {
            try 
            {
                var cacheKey = "SalesValues";
                var jsonData = _distributedCache.GetString(cacheKey);
                var allsales = JsonConvert.DeserializeObject<List<Sale>>(jsonData);
                if (allsales != null)
                {
                    return allsales;
                }
                else
                {
                    allsales = _salesDBContext.Sales.ToList();
                    jsonData = JsonConvert.SerializeObject(allsales);
                    _distributedCache.SetString(cacheKey, jsonData);
                    return allsales;
                }
            }
            catch 
            {
                return _salesDBContext.Sales.ToList();
            }
        }

        public void RemoveSale(int ID)
        {
            var saleItem = _salesDBContext.Sales.Find(ID);
            _salesDBContext.Sales.Remove(saleItem);
            _salesDBContext.SaveChanges();
        }
    }
}
