using System.Net.Http;
using System.Threading.Tasks;
using PriceKeeper;

namespace PriceKeeperBL.Parsers
{
    public interface IParser
    {
        public Measurement ParseSource(Product product);
    }
}
