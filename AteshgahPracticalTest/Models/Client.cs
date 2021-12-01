using System;
using System.Collections.ObjectModel;

namespace LoanPracticalTest.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientUniqueId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TelephoneNr { get; set; }

        public Collection<Loan> Loans { get; set; }
    }
}