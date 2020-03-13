using System;
using System.Collections.Generic;
using System.Text;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    public class ParserFactory
    {
        public IParser CreateParser(string link)
        {
            if (link.Contains("empik"))
                return new ParserEmpik();

            if (link.Contains("ochnik"))
                return new ParserOchnik();

            if (link.Contains("coffeedesk"))
                return new ParserCoffeeDesk();

            if (link.Contains("x-kom"))
                return new ParserXkom();

            if (link.Contains("steam"))
                return new ParserSteam();

            throw new Exception("Parser not-available");
        }
    }
}
