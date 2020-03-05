using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriceKeeper
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Threshold { get; set; }
        public string Size { get; set; }
        public string Link { get; set; }
    }
}
