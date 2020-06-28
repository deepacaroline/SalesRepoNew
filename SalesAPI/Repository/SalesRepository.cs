using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Data;
using SalesAPI.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NLog;

namespace SalesAPI.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private SalesDBContext _salesDBContext;
        private IDistributedCache _distributedCache;
        private ILog _appLogger;
        public SalesRepository(SalesDBContext saleDBContext, IDistributedCache distributedCache, ILog appLogger)
        {
            _salesDBContext = saleDBContext;
            _distributedCache = distributedCache;
            _appLogger = appLogger;
        }

        public void AddSale(Sale saleItem)
        {
            _appLogger.Information("Calling method AddSale");
            _salesDBContext.Sales.Add(saleItem);
            _salesDBContext.SaveChanges();
        }

        public void EditSale(Sale saleItem)
        {
            _appLogger.Information("Calling method EditSale");
            _salesDBContext.Entry(saleItem).State = EntityState.Modified;
            _salesDBContext.SaveChanges();
        }

        public Sale GetByID(int ID)
        {
            _appLogger.Information("Calling method GetByID");
            return _salesDBContext.Sales.Find(ID);
        }

        public IEnumerable<Sale> GetSales()
        {
            _appLogger.Information("Calling method GetSales");
            try 
            {
                var cacheKey = "SalesValues";
                var jsonData = _distributedCache.GetString(cacheKey);
                if(jsonData != null)
                {
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
                else
                {
                    var salesAll = _salesDBContext.Sales.ToList();
                    jsonData = JsonConvert.SerializeObject(salesAll);
                    _distributedCache.SetString(cacheKey, jsonData);
                    return salesAll;
                }
               
            }
            catch (Exception ex)
            {
                _appLogger.Error(ex.Message);
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
