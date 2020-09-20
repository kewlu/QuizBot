using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QuizBot.BLL.Contracts;
using QuizBot.BLL.Core.Models;
using Telegram.Bot.Types;
using User = QuizBot.Entities.User;
using Query = QuizBot.Entities.Query;

namespace QuizBot.BLL.Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly Dictionary<long, Quiz> _activeQuizzes = new Dictionary<long, Quiz>() ;

        private readonly IBotService _botService;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QuizService(IBotService botService, IServiceScopeFactory serviceScopeFactory)
        {
            _botService = botService;
            _serviceScopeFactory = serviceScopeFactory;
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
            using var scope = _serviceScopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetService<IUserService>();
            return (await service.GetByChatId(chatId)).
                OrderByDescending(u => u.Score);
        }

        public async Task<bool> StartQuiz(long chatId)
        {
            if (_activeQuizzes.ContainsKey(chatId)) return false;
            
            var quiz = new Quiz(_botService, this, chatId);
            await quiz.NextQuery();
            _activeQuizzes.Add(chatId, quiz);
            return true;
        }

        public async Task<bool> StopQuiz(long chatId)
        {
            if (!_activeQuizzes.ContainsKey(chatId)) return false;
                
            await _activeQuizzes[chatId].StopQuiz();
            _activeQuizzes.Remove(chatId);
            return true;
        }

        public async Task<Query> GetRandomQuery()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var queryService = scope.ServiceProvider.GetService<IQueryService>();
            var maxId = await queryService.GetMaxId();
            var randomId = new Random().Next(1, maxId);
            return await queryService.GetById(randomId);
        }

        public async Task UpdateUserScore(long userId, long chatId, int updScore, string name = null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var userService = scope.ServiceProvider.GetService<IUserService>();
            await userService.UpdateUserScore(userId, chatId, updScore, name);
        }
    }
}