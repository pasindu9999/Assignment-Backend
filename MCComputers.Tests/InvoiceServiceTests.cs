using NUnit.Framework;
using MCComputers.Application.Services;
using MCComputers.Application.Dtos;
using MCComputers.Domain.Entities; 
using MCComputers.Infrastructure; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCComputers.Tests
{
    [TestFixture]
    public class InvoiceServiceTests
    {
        private InMemoryDatabase _inMemoryDb; 
        private InvoiceService _invoiceService;

        [SetUp]
        public void Setup()
        {
            _inMemoryDb = new InMemoryDatabase(); 
            _invoiceService = new InvoiceService(_inMemoryDb); 
        }

        [Test]
        public async Task GenerateInvoice_ValidRequest_ReturnsInvoiceId()
        {
            
            var request = new InvoiceRequest
            {
                TransactionDate = DateTime.Now,
                Discount = 10,
                Products = new List<InvoiceItemDto>
                {
                    new InvoiceItemDto { ProductId = Guid.NewGuid(), ProductName = "Laptop", Quantity = 1, Price = 1200 },
                    new InvoiceItemDto { ProductId = Guid.NewGuid(), ProductName = "Mouse", Quantity = 1, Price = 25 }
                }
            };

            
            var invoiceId = await _invoiceService.GenerateInvoiceAsync(request);

            
            Assert.That(invoiceId, Is.Not.EqualTo(Guid.Empty));

            
            var savedInvoice = _inMemoryDb.Invoices.FirstOrDefault(i => i.Id == invoiceId);
            Assert.That(savedInvoice, Is.Not.Null);
            Assert.That(savedInvoice.TotalAmount, Is.EqualTo(1225)); 
            Assert.That(savedInvoice.BalanceAmount, Is.EqualTo(1215)); 
            Assert.That(savedInvoice.Items.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateInvoice_EmptyProductList_ThrowsArgumentException()
        {
         
            var request = new InvoiceRequest
            {
                TransactionDate = DateTime.Now,
                Discount = 0,
                Products = new List<InvoiceItemDto>() 
            };

            
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _invoiceService.GenerateInvoiceAsync(request));
            Assert.That(ex.Message, Is.EqualTo("Products cannot be empty"));
        }

        [Test]
        public void GenerateInvoice_NullProductList_ThrowsArgumentException()
        {
            
            var request = new InvoiceRequest
            {
                TransactionDate = DateTime.Now,
                Discount = 0,
                Products = null 
            };

            
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _invoiceService.GenerateInvoiceAsync(request));
            Assert.That(ex.Message, Is.EqualTo("Products cannot be empty"));
        }

    }
}