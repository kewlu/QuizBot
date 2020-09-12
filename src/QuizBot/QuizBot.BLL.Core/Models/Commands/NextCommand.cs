using System;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Models.Commands
{
    public class NextCommand : Command
    {
        protected override string Name { get => "/next"; set => throw new NotImplementedException(); }
        public override async Task<bool> ExecuteAsync(
            Message message,
            IBotService bot,
            IQuizService quizService)
        {
            var chatId = message.Chat.Id;
            await quizService.NextQuery(chatId);
            return true;
        }
    }
}