using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCComputers.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime TransactionDate { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}
