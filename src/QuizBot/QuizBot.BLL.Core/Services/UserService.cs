using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBot.BLL.Contracts;
using QuizBot.DAL.Contracts;
using QuizBot.DAL.EF;
using QuizBot.Entities;

namespace QuizBot.BLL.Core.Services
{
    public class UserService : IUserService
    {
        //private readonly IRepository<User> _userRepository;

        private readonly MainContext _db;

        public UserService(MainContext db)
        {
            _db = db;
        }
        
        public async Task AddUser(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<User> GetByUserId(long userId)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetByChatId(long chatId)
        {
            return await _db.Users.Where(x => x.ChatId == chatId).ToListAsync();
        }
        private async Task UpdateUser(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }

        public async Task UpdateUserScore(long userId, long chatId, int updScore, string name = null)
        {
            var chatUsers = await GetByChatId(chatId);

            var user = chatUsers?.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                user.Score += updScore;
                await UpdateUser(user);
                return;
            }

            user = new User()
            {
                UserId = userId,
                ChatId = chatId,
                Username = name,
                Score = updScore
            };
            await AddUser(user);
        }
    }
}
