using System;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Models.Commands
{
    public class StartCommand : Command
    {
        protected override string Name { get => "/start"; set => throw new NotImplementedException(); }
        public override async Task<bool> ExecuteAsync(
            Message message,
            IBotService bot,
            IQuizService quizService)
        {
            var chatId = message.Chat.Id;
            return await quizService.StartQuiz(chatId);
        }
    }
}