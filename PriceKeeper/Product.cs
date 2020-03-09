namespace PriceKeeper
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Threshold { get; set; }
        public string Size { get; set; }
        public string Link { get; set; }

        public Product(string name, string category, double threshold, string size, string link)
        {
            Name = name;
            Category = category;
            Threshold = threshold;
            Size = size;
            Link = link;
        }
    }
}
