using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using QuizBot.BLL.Core.Models;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly Dictionary<long, Quiz> _activeQuizzes = new Dictionary<long, Quiz>() ;

        private readonly IBotService _botService;
        private readonly IUserService _userService;
        private readonly IQueryService _queryService;

        public QuizService(IBotService botService,
            IUserService userService,
            IQueryService queryService)
        {
            _botService = botService;
            _userService = userService;
            _queryService = queryService;
        }
        public Task ProcessMessage(Message message)
        {
            var chatId = message.Chat.Id;
            if(!_activeQuizzes.ContainsKey(chatId)) return Task.CompletedTask;

            _activeQuizzes[chatId].SaveMessage(message);
            return Task.CompletedTask;
        }

        public async Task NextQuery(long chatId)
        {
            if(!_activeQuizzes.ContainsKey(chatId)) return;
            await _activeQuizzes[chatId].NextQuery();
        }

        public Task StartQuiz(long chatId)
        {
            if (_activeQuizzes.ContainsKey(chatId)) return Task.CompletedTask;
            
            var quiz = new Quiz(_botService, _userService, _queryService, chatId);
            _activeQuizzes.Add(chatId, quiz);
            return Task.CompletedTask;
        }

        public Task StopQuiz(long chatId)
        {
            if (!_activeQuizzes.ContainsKey(chatId)) return Task.CompletedTask;
                
            _activeQuizzes[chatId].StopQuiz().Start();
            _activeQuizzes.Remove(chatId);
            return Task.CompletedTask;
        }
    }
}