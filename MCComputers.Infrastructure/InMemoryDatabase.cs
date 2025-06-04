using MCComputers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCComputers.Infrastructure
{
    public class InMemoryDatabase
    {
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
