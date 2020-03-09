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
        
        private void AddProductToDatabase(Product product)
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
