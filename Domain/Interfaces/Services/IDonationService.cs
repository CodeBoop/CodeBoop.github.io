using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IDonationService
    {

        Task<decimal> Total();
        Task<int> Count();

        Task<IEnumerable<Donation>> Get(int latest);
        Task<IEnumerable<Donation>> Get();

        Task<Donation> GetLargest();

        Task Create(Donation donation);

    }
}
