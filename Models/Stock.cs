﻿namespace InvestSense_API.Models
{
	public class Stock
	{
		public int Id { get; set; }
		public string Symbol { get; set; } = string.Empty;
		public string CompanyName { get; set;} = string.Empty;
		public double Price { get; set; }
		public double LastDividend { get; set;}
		public string Industry { get; set; } = string.Empty;
		public long MarketCap { get; set; }

		public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
		public List<Comment> Comments { get; set; } = [];

	}
}
