using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Model
{
    public class Sale
    {
        [Key]
        public int invoiceID { get; set; }    
        [Required]
        public int productID { get; set; }
        [Required]
        [StringLength(15)] //Model Validations
        public string productName { get; set; }
        public int productQuantity { get; set; }
        public int totalPrice { get; set; }
    }
}
