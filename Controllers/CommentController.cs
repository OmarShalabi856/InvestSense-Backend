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
		public CommentController(ApplicationDbContext context, IMapper mapper, ICommentRepository commentRepository)
		{
			_context = context;
			_mapper = mapper;
			_commentRepository = commentRepository;
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
			var comment = await _commentRepository.GetByIdAsync(id);
			if (comment == null)
			{
				return NotFound();
			}
			var commentDTO = _mapper.Map<CommentDTO>(comment);
			return Ok(commentDTO);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateCommentRequestDTO createdCommentDTO)
		{
			var commentCreated = _mapper.Map<Comment>(createdCommentDTO);
			await _commentRepository.CreateAsync(commentCreated);
			return CreatedAtAction(nameof(GetById), new { commentCreated.Id }, createdCommentDTO);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var existingComment = await _commentRepository.GetByIdAsync(id);
			if (existingComment == null)
			{
				return NotFound();
			}

			await _commentRepository.DeleteAsync(id);


			return NoContent();
		}
	}
}
