using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.API
{
    

    public class EventBriteSummary
    {
        public IDictionary<string, object> Totals { get; set; }

        public decimal Net => decimal.Parse(Totals["net"].ToString());
        public int Quantity => int.Parse(Totals["quantity"].ToString());
    }
}
