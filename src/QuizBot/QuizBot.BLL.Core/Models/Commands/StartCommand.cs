using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name { get; set; }
        public override Task<bool> ExecuteAsync(Message message, IBotService bot)
        {
            throw new System.NotImplementedException();
        }

        public override bool Contains(string command)
        {
            throw new System.NotImplementedException();
        }
    }
}