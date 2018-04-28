using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_RealeState.Models
{
    public class RentPayment
    {
        public int Id { get; set; }
        public int RentId { get; set; }
        public int FlatId { get; set; }
        public int UserId { get; set; }
        public int Payment_Code { get; set; }
        public Nullable<System.DateTime> DataTime { get; set; }
        public double Amount { get; set; }
        public string UserName { get; set; }
        public string FlatName { get; set; }

        public virtual Rent Rent { get; set; }
    }
}