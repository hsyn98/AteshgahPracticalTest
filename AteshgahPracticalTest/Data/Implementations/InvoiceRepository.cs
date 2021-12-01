using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LoanPracticalTest.Data;
using LoanPracticalTest.Data.Repositories;
using LoanPracticalTest.Models;
using LoanPracticalTest.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LoanPracticalTest.Data.Implementations
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _context;

        public InvoiceRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<InvoicesViewModel>> GetLoanInvoices(int loanId)
        {
            var list = await _context.Invoices.Where(i => i.LoanId == loanId)
                .Select(i => new InvoicesViewModel()
                {
                    OrderNr = i.OrderNr,
                    InvoiceNr = i.InvoiceNr,
                    DueDate = i.DueDate,
                    Amount = i.Amount
                }).ToListAsync();

            return list;
        }

        public async Task<GeneratedInvoiceModel> CalculateInvoices(LoanDetailsViewModel model)
        {
            var unpaidAmount = model.Amount;
            var invoices =  await _context.Invoices.ToListAsync();
            
            var invoicesList = new List<Invoice>();

            for(var i=1; i<=model.LoanPeriod; i++)
            {
                invoicesList.Add(new Invoice
                {
                    OrderNr = i,
                    InvoiceNr = (invoices.Count + i).ToString().PadLeft(4, '0'),
                    DueDate = model.PayoutDate.AddMonths(i).Date,
                    Amount = Math.Round(model.Amount / model.LoanPeriod + unpaidAmount * model.InterestRate / 100, 2)
                });
                unpaidAmount -= model.Amount / model.LoanPeriod;
            }

            var resultModel = new GeneratedInvoiceModel
            {
                InvoicesList = invoicesList,
                TotalInterest = invoicesList.Sum(a => a.Amount) - model.Amount
            };
            
            return resultModel;
        }
        
        // public async Task<decimal> AmountCalculationProcedure(decimal amount, int loanPeriod, int orderNumber, decimal interestRate)
        // {
        //     var parameters = new SqlParameter
        //     {
        //         ParameterName = "ReturnVal",
        //         SqlDbType = SqlDbType.Decimal,
        //         Direction = ParameterDirection.Output,
        //     };
        //
        //     await _context.Database.ExecuteSqlRawAsync($"EXEC @returnVal = [dbo].[InvoiceCalculation] @LoanPeriod={loanPeriod}, @LoanAmount={amount}, @InvoiceOrderNr={orderNumber}, @InterestRate={interestRate}", parameters);
        //     var returnVal = (decimal)parameters.Value;
        //     return returnVal;
        // }
    }
}