using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanPracticalTest.Models;

namespace LoanPracticalTest.Data.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClients();
    }
}