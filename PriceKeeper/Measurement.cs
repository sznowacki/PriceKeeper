using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PriceKeeper
{
    class Measurement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
