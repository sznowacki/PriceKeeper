using System;
using System.Globalization;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserXkom : IParser
    {
        public Measurement ParseSource(Product product)
        {
            try
            {
                Measurement measurement = new Measurement();
                HtmlDocument document = new HtmlDocument();
                Page page = new Page();

                var site = page.GetPageAsStringAsync(product.Link).Result;
                document.LoadHtml(site);

                var value = document.DocumentNode
                    .SelectNodes("//meta[contains(@property, 'product:price:amount')]")[0]
                    .Attributes["content"]
                    .Value;

                measurement.Available = document.DocumentNode
                    .SelectNodes("//span[contains(@class, 'sc-1hdxfw1-1 cMQxDU')]")[0]
                    .InnerText
                    .Contains("Dodaj do koszyka");

                measurement.Price = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                measurement.Date = DateTimeOffset.Now;
                measurement.ProductId = product.Id;

                return measurement;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
