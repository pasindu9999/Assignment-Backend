using MCComputers.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCComputers.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<Guid> GenerateInvoiceAsync(InvoiceRequest request);
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        Task<InvoiceDto?> GetInvoiceByIdAsync(Guid id);
        Task<bool> UpdateInvoiceAsync(Guid id, InvoiceRequest request);
        Task<bool> DeleteInvoiceAsync(Guid id);
    }
}
