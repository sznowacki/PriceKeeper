using System;
using System.Collections.Generic;
using System.Text;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    interface IParser
    {
        public Measurement ParseSource(Product product);
    }
}
