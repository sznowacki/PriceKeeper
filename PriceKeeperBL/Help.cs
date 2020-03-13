using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PriceKeeper;

namespace PriceKeeperBL
{
    public class Help
    {
        public void PrintHelpMessage()
        {
            Console.WriteLine(
                "Arguments:" +
                "\n======================================================================================================" +
                "\n--add" +
                "\n\tIf you want to add a new product to the database you need to pass arguments in a specific order." +
                "\n\tExample: --add \"<Product Name>\" \"<Category>\" \"<Threshold>\" \"<Size>\" \"<Link>\"" +
                "\n" +
                "\n\t\tProduct Name \t- name of your product" +
                "\n\t\tCategory \t- category of your product, if your category is not present in current list, just simply add it" +
                "\n\t\tThreshold \t- maximum price for which you want to buy the product" +
                "\n\t\tSize \t\t- size of your product. If not applicable for your product, type N/A" +
                "\n\t\tLink \t\t- link to your product" +
                "\n======================================================================================================" +
                "\n--categories" +
                "\n\tShow currently added categories" +
                "\n ====================================================================================================== " +
                "\n--sites" +
                "\n\tShow currently supported sites"
            );
        }

        public void PrintWebsitesMessage()
        {
            Console.WriteLine(
                "www.empik.com" +
                "\nwww.ochnik.com" +
                "\nwww.coffeedesk.pl" +
                "\nwww.x-kom.pl"
            );
        }

        public void PrintCategoriesMessage()
        {
            Console.WriteLine(
                $"{Category.Books.ToString()}" +
                $"\n{Category.Clothes.ToString()}" +
                $"\n{Category.Coffee.ToString()}" +
                $"\n{Category.Electronics.ToString()}"
            );
        }
    }
}
