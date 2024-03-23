
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

				config.CreateMap<Stock, CreateStockRequestDTO>();
				config.CreateMap<CreateStockRequestDTO, Stock>();

				config.CreateMap<FMPStock, Stock>();

				config.CreateMap<Comment, CommentDTO>()
				   .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.UserName : null));

				config.CreateMap<CommentDTO, Comment>();

				config.CreateMap<Comment, CreateCommentRequestDTO>();
				config.CreateMap<CreateCommentRequestDTO, Comment>();


				
			});

			return mappingConfig;
		}
	}
}
