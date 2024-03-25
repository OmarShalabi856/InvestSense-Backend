using AutoMapper;
using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Extensions;
using InvestSense_API.Helpers;
using InvestSense_API.Models;
using InvestSense_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvestSense_API.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		public readonly ApplicationDbContext _context;
		public readonly IMapper _mapper;
		public readonly ICommentRepository _commentRepository;
		public readonly IStockRepository _stockRepository;
		public readonly UserManager<AppUser> _userManager;
		public readonly IFMP _FMPService;
		public CommentController(ApplicationDbContext context, IMapper mapper, ICommentRepository commentRepository, IStockRepository stockRepository,UserManager<AppUser> userManager,IFMP FMPService)
		{
			_context = context;
			_mapper = mapper;
			_commentRepository = commentRepository;
			_stockRepository = stockRepository;
			_userManager = userManager;
			_FMPService = FMPService;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject commentQueryObject)
		{
			var comments = await _commentRepository.GetAllAsync(commentQueryObject);
			var commentsDTO = comments.Select(c => _mapper.Map<CommentDTO>(c)).ToList();
			return Ok(commentsDTO);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var comment = await _commentRepository.GetByIdAsync(id);
			if (comment == null)
			{
				return NotFound();
			}
			var commentDTO = _mapper.Map<CommentDTO>(comment);
			return Ok(commentDTO);
		}

		[HttpPost("{stockSymbol:alpha}")]
		[Authorize]
		public async Task<IActionResult> Create([FromRoute] string stockSymbol, [FromBody] CreateCommentRequestDTO createdCommentDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var stock = await _stockRepository.GetStockBySymbol(stockSymbol);
			if (stock == null)
			{
				stock = await _FMPService.FindStockBySymbolAsync(stockSymbol);
				if(stock == null)
				{
					return BadRequest("Stock Does Not Exist!");
				}
				await _stockRepository.CreateAsync(stock);
			}
			var userName = User.GetUserName();
			var appUser = await _userManager.FindByNameAsync(userName);
			var commentCreated = _mapper.Map<Comment>(createdCommentDTO);
			commentCreated.AppUserId = appUser.Id;
			commentCreated.StockId = stock.Id;
			await _commentRepository.CreateAsync(commentCreated);
			return CreatedAtAction(nameof(GetById), new { commentCreated.Id }, createdCommentDTO);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existingComment = await _commentRepository.GetByIdAsync(id);
			if (existingComment == null)
			{
				return NotFound("Comment Does Not Exist!");
			}

			await _commentRepository.DeleteAsync(id);


			return Ok();
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDTO updateCommentRequestDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existingComment = await _commentRepository.GetByIdAsync(id);
			if (existingComment == null)
			{
				return NotFound();
			}
			var updatedComment = _mapper.Map<Comment>(updateCommentRequestDTO);
			updatedComment.Id = id;
			await _commentRepository.UpdateAsync(id, updatedComment);


			return Ok(_mapper.Map<StockDTO>(existingComment));
		}

	}
}
