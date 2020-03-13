using System;
using System.Collections.Generic;
using System.Linq;
using PriceKeeper;
using PriceKeeperBL.Parsers;

namespace PriceKeeperBL
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "--add")
                {
                    var product = CreateProductFromArguments(args);
                    AddProductToDatabase(product);
                }
                else if (args[0] == "--help")
                {
                    Help help = new Help();
                    help.PrintHelpMessage();
                }
                else if (args[0] == "--sites")
                {
                    Help help = new Help();
                    help.PrintWebsitesMessage();
                }
                else if (args[0] == "--categories")
                {
                    Help help = new Help();
                    help.PrintCategoriesMessage();
                }
            }
            else
            {
                List<Product> products = RetrieveProductsListFromDatabase();
                List<Measurement> measurements = GetMeasurementsForProducts(products);
                
                Newsletter newsletter = new Newsletter();
                var message = newsletter.CreateMessage(products,measurements);
                newsletter.SaveMessage(message);

                AddMeasurementsToDatabase(measurements);
            }
        }

        private static Product CreateProductFromArguments(string[] args)
        {
            var name = args[1];
            var category = args[2];
            var threshold = Convert.ToDouble(args[3]);
            var size = args[4];
            var link = args[5];
            
            return new Product(name, category, threshold, size, link);
        }

        private static void AddMeasurementsToDatabase(List<Measurement> measurements)
        {
            foreach (Measurement measurement in measurements)
            {
                using var dataContext = new PriceKeeperDbContext();
                dataContext.Measurement.Add(measurement);
                dataContext.SaveChanges();
            }
        }

        private static List<Measurement> GetMeasurementsForProducts(List<Product> products)
        {
            return products
                .Select(GetMeasurement)
                .Where(x => x != null)
                .ToList();
        }

        private static Measurement GetMeasurement(Product product)
        {
            ParserFactory factory = new ParserFactory();
            var parser = factory.CreateParser(product.Link);
            return parser.ParseSource(product);
        }
        
        private static void AddProductToDatabase(Product product)
        {
            using var dataContext = new PriceKeeperDbContext();
            dataContext.Product.Add(product);
            dataContext.SaveChanges();
        }
        
        private static List<Product> RetrieveProductsListFromDatabase()
        {
            using var context = new PriceKeeperDbContext();
            return context.Product.ToList();
        }
    }
}
