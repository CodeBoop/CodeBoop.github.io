using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.API
{

    public class PayPalTransaction
    {
        public string Id { get; set; }
        public Status Status { get; set; }
        public Intent Intent { get; set; }
        public PurchaseUnit[] PurchaseUnits { get; set; }
        public DateTime CreateTime { get; set; }


        public class PurchaseUnit
        {
            public string ReferenceId { get; set; }
            public Amount Amount { get; set; }

        }

        public class Amount
        {
            public string CurrencyCode { get; set; }
            public string Value { get; set; }

            public decimal? DecimalValue
            {
                get
                {
                    if (decimal.TryParse(Value, out var v))
                        return v;

                    return null;
                }
            }
        }


    }

    public enum Intent
    {
        CAPTURE,
        AUTHORIZE
    }

    public enum Status
    {
        CREATED,
        SAVED,
        APPROVED,
        VOIDED,
        COMPLETED,
        PAYER_ACTION_REQUIRED
    }

}
