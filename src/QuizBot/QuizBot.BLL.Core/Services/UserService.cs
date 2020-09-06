using System.Collections.Generic;
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

        public async Task<IEnumerable<User>> GetByTelegramId(int telegramId)
        {
            return await _userRepository.FindAsync(x => x.TelegramId == telegramId);
        }

        public async Task<IEnumerable<User>> GetByChatId(int chatId)
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
    }
}