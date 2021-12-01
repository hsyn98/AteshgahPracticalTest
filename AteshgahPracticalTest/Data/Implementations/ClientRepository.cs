using System.Collections.Generic;
using System.Threading.Tasks;
using LoanPracticalTest.Data;
using LoanPracticalTest.Data.Repositories;
using LoanPracticalTest.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanPracticalTest.Data.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _context.Clients.ToListAsync();
        }
    }
}