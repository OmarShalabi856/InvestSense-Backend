using AutoMapper;
using InvestSense_API.Data;
using InvestSense_API.DTOs;
using InvestSense_API.Models;
using InvestSense_API.Services;
using Microsoft.AspNetCore.Http;
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
		public CommentController(ApplicationDbContext context, IMapper mapper, ICommentRepository commentRepository, IStockRepository stockRepository)
		{
			_context = context;
			_mapper = mapper;
			_commentRepository = commentRepository;
			_stockRepository = stockRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var comments = await _commentRepository.GetAllAsync();
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

		[HttpPost("{stockId:int}")]
		public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDTO createdCommentDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!await _stockRepository.CheckStockExists(stockId))
			{
				return BadRequest("Stock Does Not Exist!");
			}
			var commentCreated = _mapper.Map<Comment>(createdCommentDTO);
			commentCreated.StockId = stockId;
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
