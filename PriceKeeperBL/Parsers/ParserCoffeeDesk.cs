using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserCoffeeDesk : IParser
    {
        public Measurement ParseSource(Product product)
        {
            Measurement measurement= new Measurement();
            using (WebClient client = new WebClient())
            {
                var doc = new HtmlWeb()
                    .Load(product.Link);

                measurement.Price = Convert.ToSingle(doc.DocumentNode
                    .SelectNodes("//span[contains(@itemprop, 'price')]")[0]
                    .Attributes[1]
                    .Value, CultureInfo.InvariantCulture);

                measurement.Available = doc.DocumentNode
                    .SelectNodes("//button[contains(@class, 'btn-to-cart btn-to-cart-new')]")[0]
                    .InnerText
                    .Contains("Dodaj do koszyka");

                measurement.Date = DateTimeOffset.Now;
                measurement.ProductId = product.Id;

                return measurement;
            }
        }
    }
}