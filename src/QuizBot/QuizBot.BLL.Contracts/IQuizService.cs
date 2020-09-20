using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBot.Entities;
using Telegram.Bot.Types;
using User = QuizBot.Entities.User;

namespace QuizBot.BLL.Contracts
{
    public interface IQuizService
    {
        Task ProcessMessage(Message message);
        Task<bool> NextQuery(long chatId);
        Task<bool> StartQuiz(long chatId);
        Task<bool> StopQuiz(long chatId);
        Task<Query> GetRandomQuery();
        Task UpdateUserScore(long userId, long chatId, int updScore, string name = null);
        Task<IEnumerable<User>> GetScoreByChatId(long chatId);
        
    }
}