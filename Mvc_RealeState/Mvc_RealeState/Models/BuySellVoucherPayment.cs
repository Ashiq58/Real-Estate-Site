using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_RealeState.Models
{
    public class BuySellVoucherPayment
    {
        public int Id { get; set; }
        public int FlatId { get; set; }
        public int UserId { get; set; }
        public int VoucherId { get; set; }
        public bool All_Paper { get; set; }
        public string Description { get; set; }
        public float Discount { get; set; }
        public int Amount { get; set; }
        public string  Type { get; set; }
        public string Code { get; set; }
        public Nullable<int> PaymentId { get; set; }

        public virtual Flat Flat { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual User User { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}