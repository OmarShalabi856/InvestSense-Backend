using InvestSense_API.DTOs;
using InvestSense_API.Helpers;
using InvestSense_API.Models;

namespace InvestSense_API.Services
{
	public interface IStockRepository:IRepository<Stock>
	{
	
		public Task<List<Stock>> GetAllWithCommentsAsync(StockQueryObject stockQueryObject);
		Task<bool> CheckStockExists(int stockId);
	}
}
