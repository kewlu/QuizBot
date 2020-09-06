using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuizBot.BLL.Contracts;
using QuizBot.BLL.Core.Models;
using QuizBot.BLL.Core.Models.Commands;
using Telegram.Bot;

namespace QuizBot.BLL.Core.Services
{
    public class BotService : IBotService
    {
        private readonly IQueryService _queryService;
        private readonly IUserService _userService;

        private readonly BotConfig _config;

        private readonly ILogger<BotService> _logger;

        private static List<Command> _commandsList;

        public BotService(IOptions<BotConfig> config, IQueryService queryService, IUserService userService)
        {
            _queryService = queryService;
            _userService = userService;
            _config = config.Value;
            Client = new TelegramBotClient(_config.BotToken);
            
            _logger.LogInformation("Client created: {0}", _config.BotToken);

            _commandsList = new List<Command> 
            {
                new StartCommand(),
                new ScoreCommand()
            };
        }

        public static IEnumerable<Command> Commands => _commandsList.AsReadOnly();
        
        public TelegramBotClient Client { get; }
        
        public Task InitAsync { get; }
    }
}