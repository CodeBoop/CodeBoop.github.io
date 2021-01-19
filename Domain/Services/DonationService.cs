using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services
{
    public class DonationService : IDonationService
    {

        private IDonationRepository Repo { get; }

        public DonationService(IDonationRepository repo)
        {
            Repo = repo;
        }

        public Task<decimal> Total()
        {
            return Repo.Total();
        }

        public Task<int> Count()
        {
            return Repo.Count();
        }

        public Task<IEnumerable<Donation>> Get(int latest)
        {
            return Repo.Get(latest);
        }

        public Task<IEnumerable<Donation>> Get()
        {
            return  Repo.Get();
        }

        public Task<Donation> GetLargest()
        {
            return Repo.GetLargest();
        }
    }
}
