using InvestSense_API.Models;
using InvestSense_API.Services;

namespace InvestSense_API.Repository
{
	public interface IPortfolioRepository : IRepository<Portfolio>
	{ 
		public Task<List<Stock>> GetUserPortfolio(AppUser user);
	}
}
