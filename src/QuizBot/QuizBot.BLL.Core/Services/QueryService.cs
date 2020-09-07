using System.Linq;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using QuizBot.DAL.Contracts;
using QuizBot.Entities;

namespace QuizBot.BLL.Core.Services
{
    public class QueryService : IQueryService
    {
        private readonly IRepository<Query> _queryRepository;

        public QueryService(IRepository<Query> queryRepository)
        {
            _queryRepository = queryRepository;
        }
        
        public async Task<Query> GetById(int id)
        {
            return await _queryRepository.Get(id);
        }

        public async Task<int> GetMaxId()
        {
            return (await _queryRepository.GetAll()).Max(q => q.Id);
        }
    }
}