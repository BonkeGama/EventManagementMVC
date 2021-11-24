using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Item
    {

        public Staff Product { get; set; }
        public int Quantity { get; set; }
        public Staff IsFeatured { get; set; }

    }
}