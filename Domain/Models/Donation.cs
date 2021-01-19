using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Domain.Models
{
    public class Donation
    {

        public Guid Id { get; set; }
        public string PassPhrase { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }
        public bool Anon { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Comment { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
