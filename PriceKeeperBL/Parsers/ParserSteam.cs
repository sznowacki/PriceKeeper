using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserSteam : IParser
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

                var price = document.DocumentNode.SelectNodes("//div[contains(@class, 'game_purchase_price price')]")[0]
                    .InnerHtml
                    .Replace("zł", "")
                    .Trim();

                measurement.Available = true;
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
