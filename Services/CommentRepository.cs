using InvestSense_API.Data;
using InvestSense_API.Models;

namespace InvestSense_API.Services
{
	public class CommentRepository(ApplicationDbContext context) : Repository<Comment>(context), ICommentRepository
	{
	}
}
