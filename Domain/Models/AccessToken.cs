using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AccessToken
    {

        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

    }
}
