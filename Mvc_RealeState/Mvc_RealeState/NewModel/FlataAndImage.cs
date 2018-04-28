using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_RealeState.NewModel
{
    public class FlataAndImage
    {
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
        public int PropertyName { get; set; }
        public Nullable<int> Discount { get; set; }
        public int FlatType { get; set; }
        public string Imge { get; set; }
    }
}