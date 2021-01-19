using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IDonationRepository
    {

        Task<decimal> Total();

        Task<int> Count();

        Task<IEnumerable<Donation>> Get();

        Task<IEnumerable<Donation>> Get(int latest);

        Task<Donation> GetLargest();
    }
}
