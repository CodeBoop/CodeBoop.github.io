using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.API
{


    public class EventBriteOrderPage
    {
        public Pagination pagination { get; set; }
        public Order[] orders { get; set; }
    }

    public class Pagination
    {
        public int object_count { get; set; }
        public int page_number { get; set; }
        public int page_size { get; set; }
        public int page_count { get; set; }
        public bool has_more_items { get; set; }
    }

    public class Order
    {
        public Costs costs { get; set; }
        public string resource_uri { get; set; }
        public string id { get; set; }
        public DateTime changed { get; set; }
        public DateTime created { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public object time_remaining { get; set; }
        public string event_id { get; set; }
    }

    public class Costs
    {
        public Fee base_price { get; set; }
        public Fee eventbrite_fee { get; set; }
        public Fee gross { get; set; }
        public Fee payment_fee { get; set; }
        public Fee tax { get; set; }
    }

    public class Fee
    {
        public string display { get; set; }
        public string currency { get; set; }
        public int value { get; set; }
        public string major_value { get; set; }

        public decimal ValueDecimal => ((decimal) value) / 100;
    }


}
