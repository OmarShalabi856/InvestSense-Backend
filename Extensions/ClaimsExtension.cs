using System.Security.Claims;

namespace InvestSense_API.Extensions
{
	public static class ClaimsExtension
	{
		public static string GetUserName(this ClaimsPrincipal user)
		{
			return user.Claims.SingleOrDefault(x => x.Type.Equals("https://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
		}
	}
}
