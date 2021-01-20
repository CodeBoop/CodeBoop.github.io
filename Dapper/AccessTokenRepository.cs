using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace DapperRepos
{
    public class AccessTokenRepository : BaseRepo, IAccessTokenRepository {
        public AccessTokenRepository(ConnectionStringProvider connectionString) : base(connectionString)
        {
        }

        public async Task<AccessToken> PayPal()
        {
            return (await RunAsync<AccessToken>("Select top 1 * from AccessTokens where Id='PayPal'")).FirstOrDefault();
        }
    }
}
