using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Contracts
{
    public interface IQuizService
    {
        Task ProcessMessage(Message message);
    }
}