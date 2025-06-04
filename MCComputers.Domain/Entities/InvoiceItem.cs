using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCComputers.Domain.Entities
{
    public class InvoiceItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid InvoiceId { get; set; }  
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Invoice Invoice { get; set; } 
    }
}
