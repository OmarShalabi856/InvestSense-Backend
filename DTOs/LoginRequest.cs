using System.ComponentModel.DataAnnotations;

namespace InvestSense_API.DTOs
{
	public class LoginRequest
	{

		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }
	}
}
