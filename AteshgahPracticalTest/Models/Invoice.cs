using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanPracticalTest.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNr { get; set; }
        public int OrderNr { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }

        public int LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}