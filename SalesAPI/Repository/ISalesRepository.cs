using SalesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Repository
{
    public interface ISalesRepository
    {
        IEnumerable<Sale> GetSales();
        Sale GetByID(int ID);
        void AddSale(Sale saleItem);
        void EditSale(Sale saleItem);
        void RemoveSale(int ID);

    }
}
