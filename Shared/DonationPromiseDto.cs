using System.Linq;
using Domain.Helpers;

namespace DTOs
{
    public class DonationPromiseDto
    {
        public string Name { get; set; }
        public bool Anon { get; set; }
        public string PaypalId { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }

    public static class DonationHelpers
    {

        public static string Firstname(this DonationPromiseDto dto)
        {
            if (dto.Name.IsNullOrWhiteSpace())
                return null;

            return dto.Name.Split(" ").FirstOrDefault();
        }

        public static string Lastname(this DonationPromiseDto dto)
        {
            if (dto.Name.IsNullOrWhiteSpace() || !dto.Name.Contains(" "))
                return null;

            return dto.Name.Split(" ").Skip(1).Join(" ");
        }
    }
}
