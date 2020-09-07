using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using QuizBot.DAL.Contracts;
using QuizBot.Entities;

namespace QuizBot.BLL.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task AddUser(User user)
        {
            await _userRepository.Create(user);
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<IEnumerable<User>> GetByUserId(long userId)
        {
            return await _userRepository.FindAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetByChatId(long chatId)
        {
            return await _userRepository.FindAsync(x => x.ChatId == chatId);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.Update(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
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
                Name = name,
                Score = updScore
            };

            await AddUser(user);
        }
    }
}