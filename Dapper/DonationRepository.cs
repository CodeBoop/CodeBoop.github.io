using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace DapperRepos
{
    public class DonationRepository :BaseRepo, IDonationRepository
    {
        public DonationRepository(ConnectionStringProvider connectionString) : base(connectionString)
        {
        }


        public async Task<decimal> Total()
        {
            return (await RunAsync<decimal>("select coalesce(sum(total),0) from donations")).FirstOrDefault();
        }

        public async Task<int> Count()
        {
            return (int)(await RunAsync<decimal>("select coalesce(count(total),0) from donations")).FirstOrDefault();
        }

        public Task<IEnumerable<Donation>> Get()
        {
            return RunAsync<Donation>("select * from Donations where Display=1 order CreatedAt desc");
        }

        public Task<IEnumerable<Donation>> Get(int latest)
        {
            return RunAsync<Donation>($"select top {latest} * from Donations where Display=1 order by CreatedAt desc");
        }

        public async Task<Donation> GetLargest()
        {
            return (await RunAsync<Donation>($"select top 1 * from Donations where Display=1 order by Total desc")).FirstOrDefault();
        }

        public Task Create(Donation donation)
        {
            return Execute(
                @"insert into Donations(Total, PassPhrase, Email, Firstname, Lastname, Comment, Anon, PayPayToken)
    values(@Total, @PassPhrase, @Email, @Firstname, @Lastname, @Comment, @Anon, @PayPayToken)", donation);
        }
    }
}
