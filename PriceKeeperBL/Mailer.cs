using System.Collections.Generic;
using PriceKeeper;

namespace PriceKeeperBL
{
    class Mailer
    {
        public void SendMail(string message)
        {
            //TODO: Send Mail based on threshold
        }

        public string CreateMessage(List<Product> products, List<Measurement> measurements)
        {
            string message =
                "<!DOCTYPE html>" +
                "<html lang = \"en\" >" +
                "<head>" +
                "<title> Price - Keeper </title>" +
                "</head>" +
                "<body>";

            foreach (Measurement measurement in measurements)
            {
                if (products[measurement.ProductId - 1].Threshold >= measurement.Price)
                {
                    message += $"<a href = \"{products[measurement.ProductId - 1].Link}\">{products[measurement.ProductId - 1].Name}</a>" +
                               $"<p>Price: {measurement.Price}</p>";
                }
            }
            message += "</body>" + "</html> ";

            return message;
        }
    }
}
