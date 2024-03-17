using InvestSense_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvestSense_API.Services
{
	public class TokenService : ITokenService
	{
		public readonly IConfiguration _configuration;
		public readonly SymmetricSecurityKey _key;
		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]!));
		}

		public string CreateToken(AppUser user)
		{
			var claims = new List<Claim>()
			{
				new(JwtRegisteredClaimNames.Email,user.Email!),
				new(JwtRegisteredClaimNames.GivenName,user.UserName!),
				
			};

			var signCred = new SigningCredentials(_key,SecurityAlgorithms.HmacSha256);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddMinutes(20),
				SigningCredentials=signCred,
				Issuer = _configuration["JWT:Issuer"],
				Audience = _configuration["JWT:Audience"],
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
