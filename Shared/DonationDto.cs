using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace DTOs
{
    public class DonationDto
    {

        public decimal Total { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
    }

    public static class DonationHelper
    {
        public static DonationDto ToDto(this Donation dom)
        {
            return new DonationDto()
            {
                Comment = dom.Comment,
                Name = dom.Anon?"Anon": $"{dom.Firstname} {dom.Lastname}",
                Total = dom.Total
            };
        }

        public static IEnumerable<DonationDto> ToDto(this IEnumerable<Donation> dom)
        {
            return dom.Select(ToDto);
        }
    }

}
