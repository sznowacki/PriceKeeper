using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    class ParserOchnik : IParser
    {
        public Measurement ParseSource(Product product)
        {
            Measurement measurement = new Measurement();
            using (WebClient client = new WebClient())
            {
                var doc = new HtmlWeb()
                    .Load(product.Link);

                var measurementData = doc.DocumentNode.SelectNodes("//div[contains(@class, 'product__data js--datalayer')]")[0];

                measurement.Price = float.Parse(measurementData.ChildNodes["input"].Attributes[2].Value, CultureInfo.InvariantCulture);

                var sizeNodes = doc.DocumentNode.SelectNodes(
                    "//select[contains(@class, 'js--outpost-popup__size outpost-popup__size')]");

                foreach (var sizeNode in sizeNodes)
                {
                    foreach (var childNode in sizeNode.ChildNodes)
                    {
                        if (childNode.InnerHtml.Trim().Contains("43")
                            || childNode.InnerHtml.Trim().Contains("L")
                            && !childNode.InnerHtml.Trim().Contains("XL"))
                        {
                            string size = childNode.InnerHtml.Replace("\t", "").Replace("\n", "");
                            measurement.Available = !size.Contains("wyprzedany");
                        }
                    }
                }

                measurement.Date = DateTimeOffset.Now;
                measurement.ProductId = product.Id;

                return measurement;
            }
        }
    }
}
