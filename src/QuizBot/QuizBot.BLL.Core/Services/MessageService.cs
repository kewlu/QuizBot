using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBot.BLL.Contracts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizBot.BLL.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBotService _bot;

        private readonly IQuizService _quizService;
        
        private readonly ILogger<MessageService> _logger;
        public MessageService(IBotService bot, IQuizService quizService,
            ILogger<MessageService> logger)
        {
            _bot = bot;
            _quizService = quizService;

            _logger = logger;
        }
        
        public async Task GetMessage(Update update)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }

            var message = update.Message;
            _logger.LogInformation("Message from {0}: {1}", message.Chat.Id, message.Text);

            foreach (var command in BotService.Commands)
            {
                if (!command.Contains(message.Text)) continue;
                _logger.LogInformation(command + "==" + message.Text + "Execute Command");
                if (await command.ExecuteAsync(message, _bot, _quizService))
                    return;
                
                await _bot.SendMessage(message.Chat.Id, "execution error");
                _logger.LogInformation("m");
            }

            await _quizService.ProcessMessage(message);
        }
    }
}