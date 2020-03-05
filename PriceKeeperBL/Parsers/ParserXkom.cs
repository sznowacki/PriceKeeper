using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserXkom : IParser
    {
        public Measurement ParseSource(Product product)
        {
            Measurement measurement = new Measurement();
            using (WebClient client = new WebClient())
            {
                var doc = new HtmlWeb()
                    .Load(product.Link);

                measurement.Price = Convert.ToSingle(doc.DocumentNode
                    .SelectNodes("//meta[contains(@property, 'product:price:amount')]")[0]
                    .Attributes["content"]
                    .Value, CultureInfo.InvariantCulture);

                measurement.Available = doc.DocumentNode.SelectNodes("//span[contains(@class, 'sc-1hdxfw1-1 cMQxDU')]")[0]
                    .InnerText
                    .Contains("Dodaj do koszyka");

                measurement.Date = DateTimeOffset.Now;
                measurement.ProductId = product.Id;

                return measurement;
            }
        }
    }
}
