﻿
using AutoMapper;
using InvestSense_API.DTOs;
using InvestSense_API.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;

namespace InvestSense_API
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<Stock, StockDTO>();
				config.CreateMap<StockDTO, Stock>();
			});

			return mappingConfig;
		}
	}
}