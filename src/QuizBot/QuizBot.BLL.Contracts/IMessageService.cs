using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Contracts
{
    public interface IMessageService
    {
        Task GetMessage(Update update);
    }
}