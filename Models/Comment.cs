namespace InvestSense_API.Models
{
	public class Comment
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string Conent { get; set; } = string.Empty;

		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public int? StockId { get; set; }
		public Stock? Stock { get; set; }
	}
}