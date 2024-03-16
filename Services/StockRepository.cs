using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestSense_API.Services
{
	public class StockRepository(ApplicationDbContext context) : Repository<Stock>(context), IStockRepository
	{

		public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO updatedEntity)
		{
			var existingEntity = await GetByIdAsync(id);
			if (existingEntity == null)
			{
				return null;
			}
			context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
			await context.SaveChangesAsync();
			return existingEntity;

		}
	}
}
