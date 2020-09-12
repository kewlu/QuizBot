using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Models.Commands
{
    public abstract class Command
    {
        protected abstract string Name { get; set; }

        public abstract Task<bool> ExecuteAsync(Message message, IBotService bot, IQuizService quizService);

        public virtual bool Contains(string command)
        {
            return (command == Name);
        }
    }
}