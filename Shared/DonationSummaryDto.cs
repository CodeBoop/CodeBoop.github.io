using System.Collections.Generic;

namespace DTOs
{
    public class DonationSummaryDto
    {

        public decimal Total { get; set; }
        public int Count { get; set; }

        public IEnumerable<DonationDto> Last10Donations { get; set; }
        public DonationDto Largest { get; set; }

    }
}
