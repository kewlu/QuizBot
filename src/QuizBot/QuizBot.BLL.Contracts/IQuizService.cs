using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Contracts
{
    public interface IQuizService
    {
        Task ProcessMessage(Message message);
        Task NextQuery(long chatId);
        Task StartQuiz(long chatId);
        Task StopQuiz(long chatId);
    }
}