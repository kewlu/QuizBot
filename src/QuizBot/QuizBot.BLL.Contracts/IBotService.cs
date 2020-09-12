using System.Threading.Tasks;

namespace QuizBot.BLL.Contracts
{
    public interface IBotService
    {
        Task InitAsync { get; }

        Task SendMessage(long chatId, string text, int? reply = null);
    }
}