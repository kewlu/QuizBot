using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBot.Entities;

namespace QuizBot.BLL.Contracts
{
    public interface IUserService
    {
        Task AddUser(User user);

        Task<User> GetById(int id);

        Task<IEnumerable<User>> GetByTelegramId(int telegramId);

        Task<IEnumerable<User>> GetByChatId(int chatId);

        Task UpdateUser(User user);

        Task<IEnumerable<User>> GetAll();
    }
}