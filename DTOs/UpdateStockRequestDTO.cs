using System.ComponentModel.DataAnnotations;

namespace InvestSense_API.DTOs
{
	public class UpdateStockRequestDTO
	{
		[Required(ErrorMessage = "Symbol is required")]
		public string Symbol { get; set; } = string.Empty;

		[Required(ErrorMessage = "Company name is required")]
		public string CompanyName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Price is required")]
		[Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1000000")]
		public double Price { get; set; }

		[Required(ErrorMessage = "Last dividend is required")]
		[Range(0, 100, ErrorMessage = "Last dividend must between 0 and 100")]
		public double LastDividend { get; set; }

		[Required(ErrorMessage = "Industry is required")]
		public string Industry { get; set; } = string.Empty;

		[Required(ErrorMessage = "Market cap is required")]
		[Range(0, 1000000000000000, ErrorMessage = "Market cap must between 0 and 1000000000000000")]
		public long MarketCap { get; set; }

	}
}
