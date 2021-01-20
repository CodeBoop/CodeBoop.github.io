using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IAccessTokenService
    {

        Task<AccessToken> PayPal();

    }
}
