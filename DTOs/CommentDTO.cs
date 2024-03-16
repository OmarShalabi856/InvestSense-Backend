using InvestSense_API.Models;

namespace InvestSense_API.DTOs
{
	public class CommentDTO
	{
		public string Title { get; set; } = string.Empty;

		public string Content { get; set; } = string.Empty;

		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public int? StockId { get; set; }
	}
}
