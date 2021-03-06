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
    
    public partial class Flat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flat()
        {
            this.Buy_Sell = new HashSet<Buy_Sell>();
            this.FlatImages = new HashSet<FlatImage>();
            this.Rents = new HashSet<Rent>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int Total_Room { get; set; }
        public string BedRoom { get; set; }
        public string Kichen { get; set; }
        public string WashRoom { get; set; }
        public string Dining { get; set; }
        public string Draining { get; set; }
        public string Corridor { get; set; }
        public string Position { get; set; }
        public string Floor_No { get; set; }
        public Nullable<bool> Complete { get; set; }
        public string Description { get; set; }
        public bool Sold { get; set; }
        public double Price { get; set; }
        public int PropertyId { get; set; }
        public Nullable<int> DiscountId { get; set; }
        public int FlatTypeId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Buy_Sell> Buy_Sell { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual FlatType FlatType { get; set; }
        public virtual Property Property { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FlatImage> FlatImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
