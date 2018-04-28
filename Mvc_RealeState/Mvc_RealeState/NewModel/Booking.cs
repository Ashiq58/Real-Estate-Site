using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc_RealeState.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc_RealeState.NewModel
{
    public class Booking
    {
        public  int Id { get; set; }
        public string UserName { get; set; }
        public string UsertAddress { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int? NID_or_Password { get; set; }
        public string CityName { get; set; }
        public string FlatName { get; set; }
        public string PropertyName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Code { get; set; }
        public double Discount { get; set; }
        public int? Allpepar { get; set; }

    }
}