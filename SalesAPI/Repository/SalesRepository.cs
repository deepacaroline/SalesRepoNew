using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Data;
using SalesAPI.Model;

namespace SalesAPI.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private SalesDBContext _salesDBContext;
        public SalesRepository(SalesDBContext saleDBContext)
        {
            _salesDBContext = saleDBContext;
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
            return _salesDBContext.Sales.ToList();
        }

        public void RemoveSale(int ID)
        {
            var saleItem = _salesDBContext.Sales.Find(ID);
            _salesDBContext.Sales.Remove(saleItem);
            _salesDBContext.SaveChanges();
        }
    }
}
