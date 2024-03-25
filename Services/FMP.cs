using AutoMapper;
using InvestSense_API.Models;
using Newtonsoft.Json;

namespace InvestSense_API.Services
{
	public class FMP:IFMP
	{
		public readonly HttpClient _httpClient;
		public readonly IConfiguration _config;
		public readonly IMapper _mapper;
		public IStockRepository _stockRepository;

		public FMP(HttpClient httpClient,IConfiguration config,IMapper mapper,IStockRepository stockRepository) 
		{

			_httpClient=httpClient;
			_config=config;
			_mapper = mapper;
			_stockRepository=stockRepository;
		}

		public async Task<Stock> FindStockBySymbolAsync(string symbol)
		{
			try
			{
				var url = $"{_config["API:FMP"]}/profile/{symbol}?&apikey={_config["APIKeys:FMP"]}";
				var result = await _httpClient.GetAsync(url);

				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
					var fmpStock = tasks[0];
					if (fmpStock != null)
					{
						var stockModel = _mapper.Map<Stock>(fmpStock);
						if (stockModel.Industry == null)
							stockModel.Industry = "";
						return stockModel;
					}
					return null;
					
				}
				return null;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}


		}
		
	}
}
