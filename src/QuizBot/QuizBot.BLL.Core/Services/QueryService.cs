using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBot.BLL.Contracts;
using QuizBot.DAL.Contracts;
using QuizBot.DAL.EF;
using QuizBot.Entities;

namespace QuizBot.BLL.Core.Services
{
    public class QueryService : IQueryService
    {
        private readonly MainContext _db;

        public QueryService(MainContext db)
        {
            _db = db;
        }
        
        public async Task<Query> GetById(int id)
        {
            return await _db.Queries.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetMaxId()
        {
            return await _db.Queries.MaxAsync(q => q.Id);
        }
    }
}