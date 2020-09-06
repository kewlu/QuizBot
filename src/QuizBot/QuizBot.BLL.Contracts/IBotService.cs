using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace QuizBot.BLL.Contracts
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }

        Task InitAsync { get; }
    }
}