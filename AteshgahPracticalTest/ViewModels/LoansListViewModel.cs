using System;

namespace LoanPracticalTest.ViewModels
{
    public class LoansListViewModel
    {
        public int LoanId { get; set; }
        public string ClientId { get; set; }
        public string Client { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayoutDate { get; set; }
    }
}