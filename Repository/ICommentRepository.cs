using InvestSense_API.Helpers;
using InvestSense_API.Models;

namespace InvestSense_API.Services
{
	public interface ICommentRepository:IRepository<Comment>
	{
		Task<List<Comment>> GetAllAsync(CommentQueryObject commentQueryObject);
	}
}
