using InvestSense_API.Data;
using InvestSense_API.Helpers;
using InvestSense_API.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestSense_API.Services
{
	public class CommentRepository(ApplicationDbContext context) : Repository<Comment>(context), ICommentRepository
	{
		public async new Task<List<Comment>> GetAllAsync(CommentQueryObject commentQueryObject)
		{
			var comments=  context.Comment.Include(i=>i.AppUser).AsQueryable();
			if (!string.IsNullOrEmpty(commentQueryObject.Symbol))
			{
				comments = comments.Where(s => s.Stock!.Symbol.Equals(commentQueryObject.Symbol));
			}

			if (commentQueryObject.Descending == true)
			{
				comments=comments.OrderByDescending(c => c.CreatedOn);
			}

			return await comments.ToListAsync();
		}

		public async new Task<Comment> GetByIdAsync(int id)
		{
			return await context.Comment.Include(i => i.AppUser).FirstOrDefaultAsync(i => i.Id == id);
		}
	}
}
