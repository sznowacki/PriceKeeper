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
            List<Product> products = RetrieveProductsListFromDatabase();
            List<Measurement> measurements = GetMeasurementsForProducts(products);

            AddMeasurementsToDatabase(measurements);
        }

        private static void AddMeasurementsToDatabase(List<Measurement> measurements)
        {
            foreach (Measurement measurement in measurements)
            {
                using (var dataContext = new PriceKeeperDbContext())
                {
                    dataContext.Measurement.Add(measurement);
                    dataContext.SaveChanges();
                }
            }
        }

        private static List<Measurement> GetMeasurementsForProducts(List<Product> products)
        {
            IParser parser = null;
            List<Measurement> measurements = new List<Measurement>();

            foreach (Product product in products)
            {
                parser = GetParser(product, parser);

                DoMeasurement(parser, product, measurements);
            }

            return measurements;
        }

        private static void DoMeasurement(IParser parser, Product product, List<Measurement> measurements)
        {
            try
            {
                Measurement measurement = parser.ParseSource(product);
                measurements.Add(measurement);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{product.Name}: {exception.Message}");
            }
        }

        private static IParser GetParser(Product product, IParser parser)
        {
            if (product.Link.Contains("empik"))
                parser = new ParserEmpik();

            else if (product.Link.Contains("ochnik"))
                parser = new ParserOchnik();

            else if (product.Link.Contains("coffeedesk"))
                parser = new ParserCoffeeDesk();

            else if (product.Link.Contains("x-kom"))
                parser = new ParserXkom();

            return parser;
        }

        private void AddProductToDatabase(Product product)
        {
            using (var dataContext = new PriceKeeperDbContext())
            {
                try
                {
                    dataContext.Product.Add(product);
                    dataContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{product.Name}: {exception.Message}");
                }
            }
        }
        
        private static List<Product> RetrieveProductsListFromDatabase()
        {
            using (var context = new PriceKeeperDbContext())
            {

            }
            var _context = new PriceKeeperDbContext();
            var productsContext = _context.Product.ToList();
            List<Product> products = new List<Product>();
            foreach (Product product in productsContext)
            {
                products.Add(product);
            }

            return products;
        }
    }
}
