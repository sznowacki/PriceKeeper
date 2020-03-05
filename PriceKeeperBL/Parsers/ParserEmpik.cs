using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserEmpik : IParser
    {
        public Measurement ParseSource(Product product)
        {
            Measurement measurement = new Measurement();
            
            using (WebClient client = new WebClient())
            {
                var doc = new HtmlWeb()
                    .Load(product.Link);

                measurement.Price = Convert.ToSingle(doc.DocumentNode
                    .SelectNodes("//div[contains(@class, 'productPriceInfo__wrapper')]")[0]
                    .InnerText
                    .Split("&")[0]
                    .Replace("\n", ""));

                measurement.Available = !(doc.DocumentNode
                    .SelectNodes("//div[contains(@class, 'productPriceInfo__shipment')]")[0]
                    .InnerText
                    .Contains("Niedostępne"));

                measurement.Date = DateTimeOffset.Now;
                measurement.ProductId = product.Id;

                return measurement;
            }
        }
    }
}
