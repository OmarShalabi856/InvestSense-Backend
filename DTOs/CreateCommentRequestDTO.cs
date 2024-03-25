using System.ComponentModel.DataAnnotations;

namespace InvestSense_API.DTOs
{
	public class CreateCommentRequestDTO
	{
		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Content is required")]
		public string Content { get; set; } = string.Empty;

	}
}
