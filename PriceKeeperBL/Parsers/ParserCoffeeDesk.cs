using System;
using System.Globalization;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserCoffeeDesk : IParser
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
                    .SelectNodes("//span[contains(@itemprop, 'price')]")[0]
                    .Attributes[1]
                    .Value;

                measurement.Available = document.DocumentNode
                    .SelectNodes("//button[contains(@class, 'btn-to-cart btn-to-cart-new')]")[0]
                    .InnerText
                    .Contains("Dodaj do koszyka");

                measurement.Price = Convert.ToDouble(price, CultureInfo.InvariantCulture);
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