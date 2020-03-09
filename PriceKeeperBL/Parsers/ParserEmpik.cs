using System;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserEmpik : IParser
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

                var price = document.DocumentNode
                    .SelectNodes("//div[contains(@class, 'productPriceInfo__wrapper')]")[0]
                    .InnerText
                    .Split("&")[0]
                    .Replace("\n", "");

                measurement.Available = !(document.DocumentNode
                    .SelectNodes("//div[contains(@class, 'productPriceInfo__shipment')]")[0]
                    .InnerText
                    .Contains("Niedostępne"));

                measurement.Price = Convert.ToDouble(price);
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
