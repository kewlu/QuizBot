using System;
using System.Linq;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Models.Commands
{
    public class ScoreCommand : Command
    {
        protected override string Name { get => "/score"; set => throw new NotImplementedException(); }
        public override async Task<bool> ExecuteAsync(
            Message message,
            IBotService bot,
            IQuizService quizService)
        {
            var chatId = message.Chat.Id;
            var userList = await quizService.GetScoreByChatId(chatId);
            
            var scoreTable = userList.Aggregate("", (current, user) => 
                current + $"{user.Username} : {user.Score}\n");

            await bot.SendMessage(chatId, scoreTable);
            return true;
        }
    }
}