using System.Linq;
using System.Threading.Tasks;
using LoanPracticalTest.Data.Repositories;
using LoanPracticalTest.Models;
using LoanPracticalTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace LoanPracticalTest.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanRepository _loanRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public LoanController(
            ILogger<LoanController> logger,
            ILoanRepository loanRepository,
            IClientRepository clientRepository,
            IInvoiceRepository invoiceRepository)
        {
            _logger = logger;
            _loanRepository = loanRepository;
            _clientRepository = clientRepository;
            _invoiceRepository = invoiceRepository;
        }

        private void LoadClients()
        {
            ViewBag.Clients =  _clientRepository.GetAllClients().Result.Select(c => new SelectListItem
            {
                Text = c.Name + " " + c.Surname,
                Value = c.Id.ToString()
            });
        }

        private void LoadLoanPeriods()
        {
            var loanPeriods = Enumerable.Range(3, 22).ToList();
            
            ViewBag.LoanPeriods = loanPeriods.Select(l => new SelectListItem
            {
                Text = l.ToString(),
                Value = l.ToString()
            });
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _loanRepository.GetLoanList();

            return View(model);
        }
        
        // GET
        public async Task<IActionResult> Details(int loanId)
        {
            var loanModel = await _loanRepository.GetLoanDetails(loanId);
            loanModel.InvoicesList = await _invoiceRepository.GetLoanInvoices(loanId);

            LoadClients();
            LoadLoanPeriods();

            return View(loanModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadClients();
            LoadLoanPeriods();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoanDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loanModel = new Loan()
                {
                    Amount = model.Amount,
                    InterestRate = model.InterestRate,
                    LoanPeriod = model.LoanPeriod,
                    PayoutDate = model.PayoutDate,
                    ClientId = model.ClientId,
                    Invoices = _invoiceRepository.CalculateInvoices(model).Result.InvoicesList
                };

                var resultModel = await _loanRepository.Add(loanModel);

                return RedirectToAction("Details", new { loanId = resultModel.Id });
            }
            
            _logger.LogError(ModelState.ValidationState.ToString());

            LoadClients();
            LoadLoanPeriods();
            
            return View(model);
        }
    }
}