//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mvc_RealeState.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Buy_Sell
    {
        public int Id { get; set; }
        public int FlatId { get; set; }
        public int UserId { get; set; }
        public int VoucherId { get; set; }
        public bool All_Paper { get; set; }
        public string Description { get; set; }
        public Nullable<int> PaymentId { get; set; }
    
        public virtual Flat Flat { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual User User { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}