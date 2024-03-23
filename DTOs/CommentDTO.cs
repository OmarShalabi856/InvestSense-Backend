using System.ComponentModel.DataAnnotations;

namespace InvestSense_API.DTOs
{
	public class CommentDTO
	{
		[Required(ErrorMessage = "Title is required")]
		[MinLength(5, ErrorMessage = "Title should be at least 5 characters")]
		[MaxLength(100, ErrorMessage = "Title should be at most 100 characters")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Content is required")]
		[MinLength(5, ErrorMessage = "Content should be at least 5 characters")]
		[MaxLength(300, ErrorMessage = "Content should be at most 300 characters")]
		public string Content { get; set; } = string.Empty;

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "StockId is required")]
		public int StockId { get; set; }


		public string CreatedBy { get; set; } = string.Empty;
	}
}
