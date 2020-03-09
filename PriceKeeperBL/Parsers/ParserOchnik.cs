using System;
using System.Globalization;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserOchnik : IParser
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
                    .SelectNodes("//div[contains(@class, 'product__data js--datalayer')]")[0]
                    .ChildNodes["input"]
                    .Attributes[2]
                    .Value;

                var sizeNodes = document.DocumentNode
                    .SelectNodes("//select[contains(@class, 'js--outpost-popup__size outpost-popup__size')]");

                foreach (var sizeNode in sizeNodes)
                {
                    foreach (var childNode in sizeNode.ChildNodes)
                    {
                        if (childNode.InnerHtml.Trim().Contains("43")
                            || childNode.InnerHtml.Trim().Contains("L")
                            && !childNode.InnerHtml.Trim().Contains("XL"))
                        {
                            var size = childNode.InnerHtml.Replace("\t", "").Replace("\n", "");
                            measurement.Available = !size.Contains("wyprzedany");
                        }
                    }
                }

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
