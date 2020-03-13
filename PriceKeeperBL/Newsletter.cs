using System;
using System.Collections.Generic;
using System.IO;
using PriceKeeper;

namespace PriceKeeperBL
{
    class Newsletter
    {
        public void SaveMessage(string message)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Price-Keeper-Message.txt")))
            {
                outputFile.WriteLine(message);
            }
        }
        public string CreateMessage(List<Product> products, List<Measurement> measurements)
        {
            string message = "";
            foreach (Measurement measurement in measurements)
            {
                if (products[measurement.ProductId - 1].Threshold >= measurement.Price)
                {
                    message += $"{products[measurement.ProductId - 1].Name}: {measurement.Price} PLN\n" +
                               $"Link: {products[measurement.ProductId - 1].Link}\n\n";
                }
            }
            return message;
        }
    }
}
