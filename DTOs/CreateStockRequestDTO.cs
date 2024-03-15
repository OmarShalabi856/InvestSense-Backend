namespace InvestSense_API.DTOs
{
	public class CreateStockRequestDTO
	{
		public string Symbol { get; set; } = string.Empty;
		public string CompanyName { get; set; } = string.Empty;
		public double Price { get;set; }
		public double LastDividend { get; set; }
		public string Industry { get; set; } = string.Empty;
		public long MarketCap { get; set; }

	}
}
