using InvestSense_API.Models;

namespace InvestSense_API.Services
{
	public interface ITokenService
	{
		public string CreateToken(AppUser user);
	}
}
