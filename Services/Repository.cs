
using InvestSense_API.Data;
using Microsoft.EntityFrameworkCore;

namespace InvestSense_API.Services
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<T> CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();

			return entity;

		}

		public async Task<T?> DeleteAsync(int id)
		{
			var existingEntity = await GetByIdAsync(id);
			if (existingEntity == null)
			{
				return null;
			}
			_context.Set<T>().Remove(existingEntity);
			await _context.SaveChangesAsync();
			return existingEntity;
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T?> UpdateAsync(int id, T updatedEntity)
		{
			var existingEntity = await GetByIdAsync(id);
			if (existingEntity == null)
			{
				return null;
			}
			_context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
			await _context.SaveChangesAsync();
			return existingEntity;
		}
	}
}
