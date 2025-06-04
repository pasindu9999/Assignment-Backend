using MCComputers.Application.Dtos;
using MCComputers.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateInvoice([FromBody] InvoiceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            try
            {
                var invoiceId = await _invoiceService.GenerateInvoiceAsync(request);
                return Ok(new { InvoiceId = invoiceId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while generating the invoice.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(Guid id, [FromBody] InvoiceRequest request)
        {
            var result = await _invoiceService.UpdateInvoiceAsync(id, request);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var result = await _invoiceService.DeleteInvoiceAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
