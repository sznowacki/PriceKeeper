using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriceKeeper
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Threshold { get; set; }
        public string Size { get; set; }
        public string Link { get; set; }

        public Product(string Name, string Category, double Threshold, string Size, string Link)
        {
            this.Name = Name;
            this.Category = Category;
            this.Threshold = Threshold;
            this.Size = Size;
            this.Link = Link;
        }
    }
}
