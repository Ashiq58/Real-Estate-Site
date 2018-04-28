using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_RealeState.NewModel
{
    public class RentPaymentInfo
    {
        public string DateTime { get; set; }
        public string Payment_Code { get; set; }
        public double Amount { get; set; }
        public int FlatId { get; set; }
        public int UserId { get; set; }
        public string Contact { get; set; }
        public string FlatName { get; set; }
        public string UserName { get; set; }
    }
}