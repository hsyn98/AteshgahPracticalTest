using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanPracticalTest.Data.Repositories;
using LoanPracticalTest.Models;
using LoanPracticalTest.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LoanPracticalTest.Data.Implementations
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DataContext _context;

        public LoanRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoansListViewModel>> GetLoanList()
        {
            var query = from loan in _context.Loans
                join client in _context.Clients on loan.ClientId equals client.Id
                select new LoansListViewModel
                {
                    LoanId = loan.Id,
                    ClientId = client.ClientUniqueId,
                    Client = client.Name + " " + client.Surname,
                    Amount = loan.Amount,
                    PayoutDate = loan.PayoutDate
                };
            
            var loansList = await query.OrderBy(l => l.ClientId).ThenByDescending(l => l.Amount).ToListAsync().ConfigureAwait(false);
            
            return loansList;
        }

        public async Task<LoanDetailsViewModel> GetLoanDetails(int loanId)
        {
            var query = from loan in _context.Loans
                join client in _context.Clients on loan.ClientId equals client.Id
                where loan.Id == loanId
                select new LoanDetailsViewModel
                {
                    ClientId = client.Id,
                    TelephoneNr = client.TelephoneNr,
                    Amount = loan.Amount,
                    LoanPeriod = loan.LoanPeriod,
                    InterestRate = loan.InterestRate,
                    PayoutDate = loan.PayoutDate.Date
                };

            var loanDetails = await query.FirstOrDefaultAsync();

            return loanDetails;
        }

        public async Task<Loan> Add(Loan loanModel)
        {
            _context.Loans.Add(loanModel);
            await _context.SaveChangesAsync();

            return loanModel;
        }
    }
}