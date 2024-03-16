namespace InvestSense_API.DTOs
{
	public class CreateCommentRequestDTO
	{
		public string Title { get; set; } = string.Empty;

		public string Content { get; set; } = string.Empty;

		public int? StockId { get; set; }
	}
}
