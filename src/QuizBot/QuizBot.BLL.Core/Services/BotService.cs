using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QuizBot.BLL.Contracts;
using QuizBot.BLL.Core.Models;
using QuizBot.BLL.Core.Models.Commands;
using Telegram.Bot;

namespace QuizBot.BLL.Core.Services
{
    public class BotService : IBotService
    {
        private readonly BotConfig _config;
        
        private static List<Command> _commandsList;

        private Dictionary<long, Quiz> _quizzes = new Dictionary<long, Quiz>();
        
        public BotService(IOptions<BotConfig> config)
        {
            _config = config.Value;
            Client = new TelegramBotClient(_config.BotToken);
            
            _commandsList = new List<Command> 
            {
                new StartCommand(),
                new NextCommand(),
                new ScoreCommand(),
                new StopCommand()
            };
        }

        public static IEnumerable<Command> Commands => _commandsList.AsReadOnly();

        public async Task SendMessage(long chatId, string text, int? reply = null)
        {
            if (reply == null)
                await Client.SendTextMessageAsync(chatId, text);
            else
                await Client.SendTextMessageAsync(chatId, text, replyToMessageId: reply.Value);
        }
        private TelegramBotClient Client { get; }
        
        public Task InitAsync { get; }

    }
}