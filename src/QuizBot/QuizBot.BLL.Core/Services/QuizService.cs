using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using QuizBot.BLL.Core.Models;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly Dictionary<int, Quiz> _activeQuizzes;
        public async Task ProcessMessage(Message message)
        {
        }
    }
}