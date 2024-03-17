﻿namespace InvestSense_API.Services
{
	public interface IRepository<T> where T : class
	{
		Task<T> CreateAsync(T entity);
		Task<T?> DeleteAsync(int id);
		Task<List<T>> GetAllAsync();
		Task<T?> GetByIdAsync(int id);
		Task<T?> UpdateAsync(int id, T updatedEntity);
	}
}
