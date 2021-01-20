using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IAccessTokenRepository
    {
        Task<AccessToken> PayPal();

    }
}
