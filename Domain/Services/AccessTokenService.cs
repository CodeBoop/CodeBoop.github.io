using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services
{
    public class AccessTokenService : IAccessTokenService
    {

        private IAccessTokenRepository Repo { get; }

        public AccessTokenService(IAccessTokenRepository repo)
        {
            Repo = repo;
        }
        
        
        public Task<AccessToken> PayPal()
        {
            return Repo.PayPal();
        }
    }
}
