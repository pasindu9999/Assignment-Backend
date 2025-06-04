using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCComputers.Application.Dtos
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}
