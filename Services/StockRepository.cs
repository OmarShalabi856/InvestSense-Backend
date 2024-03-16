using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

		public  async Task<List<Stock>> GetAllWithCommentsAsync()
		{
			return await context.Stock.Include(s=>s.Comments).ToListAsync();	
		}

		public async Task<bool> CheckStockExists(int stockId)
		{
			return await context.Stock.AnyAsync(s=>s.Id==stockId);
		}
	}
}
