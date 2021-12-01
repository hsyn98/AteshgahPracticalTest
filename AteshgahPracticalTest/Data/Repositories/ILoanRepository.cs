using System.Collections.Generic;
using System.Threading.Tasks;
using LoanPracticalTest.Models;
using LoanPracticalTest.ViewModels;

namespace LoanPracticalTest.Data.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<LoansListViewModel>> GetLoanList();
        Task<LoanDetailsViewModel> GetLoanDetails(int loanId);
        Task<Loan> Add(Loan loanModel);
    }
}