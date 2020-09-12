using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<IEnumerable<User>> GetScoreByChatId(long chatId);
    }
}