using System;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Models.Commands
{
    public class StopCommand : Command
    {
        protected override string Name { get => "/stop"; set => throw new NotImplementedException(); }
        public override async Task<bool> ExecuteAsync(
            Message message,
            IBotService bot,
            IQuizService quizService)
        {
            var chatId = message.Chat.Id;
            if(await quizService.StopQuiz(chatId))
                await bot.SendMessage(chatId, "Викторина окончена!");
            return true;
        }
    }
}