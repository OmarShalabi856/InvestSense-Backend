using InvestSense_API.DTOs;
using InvestSense_API.Models;
using Microsoft.AspNetCore.Identity;

namespace InvestSense_API.Services
{
	public class Authentication : IAuthentication
	{
		public readonly UserManager<AppUser> _userManager;
		public readonly SignInManager<AppUser> _signInManager;

		public Authentication(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IdentityResult> AssignRole(AppUser user, string roleName)
		{
			var role = await _userManager.AddToRoleAsync(user, roleName);

			return role;
		}

		public async Task<SignInResult?> CheckPassword(AppUser user,string password)
		{
			return  await _signInManager.CheckPasswordSignInAsync(user, password,false);
		}

		public async Task<IdentityResult> CreateUser(AppUser user,string password)
		{
			return await _userManager.CreateAsync(user, password);
			
		}

		public async Task<bool> EmailExists(string email)
		{
			return await _userManager.FindByEmailAsync(email) != null;

		}

		public  AppUser? GetUser(LoginRequest loginRequest)
		{
			return  _userManager.Users.FirstOrDefault(u => u.Email.Equals(loginRequest.Email));

		}

		
	}
}
