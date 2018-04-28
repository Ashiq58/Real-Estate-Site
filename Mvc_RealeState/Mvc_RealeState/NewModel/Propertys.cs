using Mvc_RealeState.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_RealeState.NewModel
{
    public class Propertys
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Total_Flat { get; set; }
        public string Total_Floor { get; set; }
        public Nullable<bool> Lift { get; set; }
        public Nullable<bool> Generator { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Caretecar { get; set; }
        public String CityName { get; set; }
        public Nullable<bool> Parking { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string FlatType { get; set; }
        public DateTime Date { get; set; }

      
    }
}