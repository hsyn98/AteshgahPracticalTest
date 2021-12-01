using System;

namespace LoanPracticalTest.ViewModels
{
    public class InvoicesViewModel
    {
        public int OrderNr { get; set; }
        public string InvoiceNr { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
    }
}