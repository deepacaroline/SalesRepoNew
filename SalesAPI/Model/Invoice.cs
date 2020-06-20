using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Model
{
    public class Invoice
    {
        public int invoiceID { get; set; }
        public DateTime billingDate { get; set; }
        public int customerID { get; set; }
        public int sumTotal { get; set; }
        public int discount { get; set; }
        public int salesPerson { get; set; }
    }
}
