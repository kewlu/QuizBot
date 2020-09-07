using System.Threading.Tasks;
using QuizBot.Entities;

namespace QuizBot.BLL.Contracts
{
    public interface IQueryService
    {
        Task<Query> GetById(int id);

        Task<int> GetMaxId();
    }
}