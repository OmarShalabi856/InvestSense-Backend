using InvestSense_API.DTOs;
using InvestSense_API.Models;
using Microsoft.AspNetCore.Identity;

namespace InvestSense_API.Services
{
	public interface IAuthentication
	{
		public Task<IdentityResult> CreateUser(AppUser user, string password);
		public AppUser? GetUser(LoginRequest loginRequest);

		public Task<IdentityResult> AssignRole(AppUser user, string roleName);

		public Task<bool> EmailExists(string email);

		public Task<SignInResult?> CheckPassword(AppUser user,string password);


	}
}
