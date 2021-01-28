using System.Collections.Generic;
using System.Linq;
using Domain.Helpers;
using Domain.Models;

namespace DTOs
{
    public class DonationDto
    {

        public decimal? Total { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
    }

    public static class DonationHelper
    {
        public static DonationDto ToDto(this Donation dom)
        {
            if (dom == null || dom.DisplayType.HasFlag(AnonType.DontShow) || (!dom.DisplayType.HasFlag(AnonType.ShowMyName) && !dom.DisplayType.HasFlag(AnonType.ShowDonationValue) && dom.Comment.IsNullOrWhiteSpace()))
                return null;

            return new DonationDto()
            {
                Comment = dom.Comment,
                Name = dom.DisplayType.HasFlag(AnonType.ShowMyName)? $"{dom.Firstname} {dom.Lastname}" : "Anonymous Donation",
                Total = dom.DisplayType.HasFlag(AnonType.ShowDonationValue)?  dom.Total : (decimal?)null
            };
        }

        public static IEnumerable<DonationDto> ToDto(this IEnumerable<Donation> dom)
        {
            return dom.Select(ToDto).Where(i=>i!=null);
        }
    }

}
