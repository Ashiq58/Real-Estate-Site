using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_RealeState.NewModel
{
    public class UserRegistration
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public Nullable<int> NId_orPassport { get; set; }
        public int CityId { get; set; }
        public int UserTypeId { get; set; }
    }
}