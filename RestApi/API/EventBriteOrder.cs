using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.API
{

    //public class EventBriteOrderRequest
    //{
    //    public Pagination Pagination { get; set; }
    //    public IEnumerable<EventBriteOrder> Orders { get; set; }
    //}


    //public class Pagination
    //{
    //    public int ObjectCount { get; set; }
    //    public int PageNumber { get; set; }

    //    public int PageSize { get; set; }
    //    public int PageCount { get; set; }
    //    public bool HasMoreItems { get; set; }
    //}

    //public class EventBriteOrder
    //{


    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }

    //    public IDictionary<string, Cost> Costs { get; set; }

    //    public IEnumerable<Question> Questions { get; set; }
    //    public IEnumerable<Response> Answers { get; set; }


    //    public class Cost
    //    {
    //        public string Currency { get; set; }
    //        public string Value { get; set; }

    //        public decimal DecimalValue => decimal.Parse(Value) / 100;
    //    }

    //    public class Question
    //    {
    //        public int Id { get; set; }
    //        public string Label { get; set; }
    //    }

    //    public class Response
    //    {
    //        public int QuestionId { get; set; }
    //        public string Answer { get; set; }
    //    }

    //}


    public class EventBriteSummary
    {
        public IDictionary<string, object> Totals { get; set; }

        public decimal Net => decimal.Parse(Totals["net"].ToString());
        public int Quantity => int.Parse(Totals["quantity"].ToString());
    }
}
