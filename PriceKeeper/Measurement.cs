﻿using System;

namespace PriceKeeper
{
    public class Measurement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
