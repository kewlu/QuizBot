using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBot.BLL.Contracts;
using Telegram.Bot.Types;

namespace QuizBot.App.Controllers
{
    [Route("api/update")]
    public class MessageController : Controller
    {
        private readonly IMessageService _message;

        public MessageController(IMessageService message)
        {
            _message = message;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            await _message.GetMessage(update);
            return Ok();
        }
    }
}