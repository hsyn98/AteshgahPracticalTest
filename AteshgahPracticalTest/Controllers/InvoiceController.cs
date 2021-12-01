using System.Threading.Tasks;
using LoanPracticalTest.Data.Repositories;
using LoanPracticalTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoanPracticalTest.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(
            ILogger<InvoiceController> logger,
            IInvoiceRepository invoiceRepository)
        {
            _logger = logger;
            _invoiceRepository = invoiceRepository;
        }
        
        [HttpPost]
        public async Task<JsonResult> CalculateInvoices([FromBody] LoanDetailsViewModel loan)
        {
            var invoices = await _invoiceRepository.CalculateInvoices(loan);

            return Json(invoices);
        }
    }
}