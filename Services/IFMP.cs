using InvestSense_API.Models;
using System.Threading.Tasks;

namespace InvestSense_API.Services
{
	public interface IFMP
	{

		Task<Stock> FindStockBySymbolAsync(string symbol);
	}
}
