using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using QuizBot.BLL.Core.Models;
using Telegram.Bot.Types;
using User = QuizBot.Entities.User;

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

        public async Task<bool> NextQuery(long chatId)
        {
            if(!_activeQuizzes.ContainsKey(chatId)) return false;
            await _activeQuizzes[chatId].NextQuery();
            return true;
        }

        public async Task<IEnumerable<User>> GetScoreByChatId(long chatId)
        {
            return (await _userService.GetByChatId(chatId)).
                OrderByDescending(u => u.Score);
        }

        public Task<bool> StartQuiz(long chatId)
        {
            if (_activeQuizzes.ContainsKey(chatId)) return Task.FromResult(false);
            
            var quiz = new Quiz(_botService, _userService, _queryService, chatId);
            _activeQuizzes.Add(chatId, quiz);
            return Task.FromResult(true);
        }

        public async Task<bool> StopQuiz(long chatId)
        {
            if (!_activeQuizzes.ContainsKey(chatId)) return false;
                
            await _activeQuizzes[chatId].StopQuiz();
            _activeQuizzes.Remove(chatId);
            return true;
        }
    }
}