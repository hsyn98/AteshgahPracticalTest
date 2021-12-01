using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanPracticalTest.Models;
using LoanPracticalTest.ViewModels;

namespace LoanPracticalTest.Data.Repositories
{
    public interface IInvoiceRepository
    {
        Task<List<InvoicesViewModel>> GetLoanInvoices(int loanId);
        Task<GeneratedInvoiceModel> CalculateInvoices(LoanDetailsViewModel model);
    }
}