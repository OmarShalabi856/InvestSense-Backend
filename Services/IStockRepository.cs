using InvestSense_API.DTOs;
using InvestSense_API.Models;

namespace InvestSense_API.Services
{
	public interface IStockRepository:IRepository<Stock>
	{
		public Task<Stock?> UpdateAsync(int id,UpdateStockRequestDTO updatedEntity);	
	}
}
