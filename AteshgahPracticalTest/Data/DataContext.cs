using LoanPracticalTest.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanPracticalTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, ClientUniqueId = "123456A",  Name = "Huseyn", Surname = "Bayramov", TelephoneNr = "+01234568"},
                new Client { Id = 2, ClientUniqueId = "123456B",  Name = "Tural", Surname = "Ahmadov", TelephoneNr = "+05484412"}
            );
        }
        
    }
}