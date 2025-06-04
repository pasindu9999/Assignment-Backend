using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCComputers.Application.Dtos
{
    public class InvoiceRequest
    {
        public DateTime TransactionDate { get; set; }
        public List<InvoiceItemDto> Products { get; set; }
        public decimal Discount { get; set; }
    }
}
