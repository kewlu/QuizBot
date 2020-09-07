using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBot.DAL.Contracts;
using QuizBot.Entities;

namespace QuizBot.DAL.EF.Repositories
{
    public class QueryRepository : IRepository<Query>
    {
        private readonly IMainContext _db;

        public QueryRepository(IMainContext context)
        {
            _db = context;
        }
        
        public async Task<IEnumerable<Query>> GetAll()
        {
            return await _db.Queries.AsNoTracking().ToListAsync();
        }

        public async Task<Query> Get(int id)
        {
            return await _db.Queries.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task<IEnumerable<Query>> FindAsync(Expression<Func<Query, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task Create(Query item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Query item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}