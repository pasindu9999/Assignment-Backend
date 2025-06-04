using MCComputers.Application.Dtos;
using MCComputers.Application.Interfaces;
using MCComputers.Domain.Entities;
using MCComputers.Infrastructure;

namespace MCComputers.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InMemoryDatabase _db;

        public InvoiceService(InMemoryDatabase db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates an invoice
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<Guid> GenerateInvoiceAsync(InvoiceRequest request)
        {
            var total = request.Products.Sum(p => p.Quantity * p.Price);
            var balance = total - request.Discount;

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                TransactionDate = request.TransactionDate,
                Discount = request.Discount,
                TotalAmount = total,

                Items = request.Products.Select(p => new InvoiceItem
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Quantity = p.Quantity,
                    Price = p.Price
                }).ToList()
            };

            _db.Invoices.Add(invoice);
            return Task.FromResult(invoice.Id);
        }

        /// <summary>
        /// Get the list of all invoices
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            return Task.FromResult(_db.Invoices.Select(MapToDto));
        }

        /// <summary>
        /// Get a single invoice by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<InvoiceDto?> GetInvoiceByIdAsync(Guid id)
        {
            var invoice = _db.Invoices.FirstOrDefault(i => i.Id == id);
            return Task.FromResult(invoice == null ? null : MapToDto(invoice));
        }

        /// <summary>
        /// Update a particular invoice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<bool> UpdateInvoiceAsync(Guid id, InvoiceRequest request)
        {
            var invoice = _db.Invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null) return Task.FromResult(false);

            invoice.TransactionDate = request.TransactionDate;
            invoice.Discount = request.Discount;
            invoice.Items = request.Products.Select(p => new InvoiceItem
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList();

            invoice.TotalAmount = invoice.Items.Sum(i => i.Quantity * i.Price);


            return Task.FromResult(true);
        }

        /// <summary>
        /// Deletes an invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteInvoiceAsync(Guid id)
        {
            var invoice = _db.Invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null) return Task.FromResult(false);

            _db.Invoices.Remove(invoice);
            return Task.FromResult(true);
        }

        private InvoiceDto MapToDto(Invoice invoice)
        {
            decimal discountAmount = invoice.TotalAmount * (invoice.Discount / 100m);

            return new InvoiceDto
            {
                Id = invoice.Id,
                TransactionDate = invoice.TransactionDate,
                Discount = invoice.Discount,
                TotalAmount = invoice.TotalAmount,
                BalanceAmount = invoice.TotalAmount - discountAmount,
                Items = invoice.Items.Select(item => new InvoiceItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
    }

}
