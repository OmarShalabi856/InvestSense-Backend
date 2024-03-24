using InvestSense_API.DTOs;
using InvestSense_API.Models;
using InvestSense_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvestSense_API.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		public readonly IAuthentication _authentication;
		public readonly ITokenService _tokenService;

		public AccountController(IAuthentication authentication, ITokenService tokenService)
		{
			_authentication = authentication;
			_tokenService = tokenService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);

				}

				var createdUser = new AppUser() { Email = registerRequest.Email, UserName = registerRequest.Username };

				if (await _authentication.EmailExists(registerRequest.Email))
				{
					return BadRequest("Email Is Already Taken!");
				}

				var createdUserResult = await _authentication.CreateUser(createdUser, registerRequest.Password);



				if (!createdUserResult.Succeeded)
				{
					return BadRequest(createdUserResult.Errors);
				}

				var userAssignedRoleResult = await AssignRole(createdUser, "User");

				if (!userAssignedRoleResult.Succeeded)
				{
					return BadRequest(userAssignedRoleResult.Errors);
				}


				return Ok(new LoginResponse
				{
					Email = createdUser.Email,
					Username = createdUser.UserName,
					Token = _tokenService.CreateToken(createdUser)
				});


			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);

			}

		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var user = _authentication.GetUser(loginRequest);
				if (user == null)
				{
					return Unauthorized("User Not Found!");
				}

				var authenticationResult = await _authentication.CheckPassword(user,loginRequest.Password);
				if (!authenticationResult!.Succeeded)
				{
					return Unauthorized("The Email Or The Password Is Incorrect!");
				}
				return Ok(new LoginResponse
				{
					Email = user.Email,
					Username = user.UserName,
					Token = _tokenService.CreateToken(user)
				});

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}


		}

		private async Task<IdentityResult> AssignRole(AppUser createdUser, string roleName)
		{
			return await _authentication.AssignRole(createdUser, roleName);
		}
	}
}
