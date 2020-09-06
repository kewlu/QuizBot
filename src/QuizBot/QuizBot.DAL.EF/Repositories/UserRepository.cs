using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBot.DAL.Contracts;
using QuizBot.Entities;

namespace QuizBot.DAL.EF.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IMainContext _db;

        public UserRepository(IMainContext context)
        {
            _db = context;
        }
        
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _db.Users.Where(predicate).ToListAsync();
        }

        public async Task Create(User item)
        {
            await _db.Users.AddAsync(item);
        }

        public async Task Update(User item)
        {
            _db.Users.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(o => o.Id == id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

        }
    }
}