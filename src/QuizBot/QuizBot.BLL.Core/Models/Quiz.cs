using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBot.BLL.Contracts;
using QuizBot.Entities;
using System.Timers;
using QuizBot.BLL.Core.Services;
using Telegram.Bot.Types;

namespace QuizBot.BLL.Core.Models
{
    public class Quiz
    {
        private readonly int _chatId;

        private readonly IBotService _bot;
        private readonly IUserService _userService;
        private readonly IQueryService _queryService;

        private Timer _hintTimer;
        private Timer _updateTimer;

        private Query _currentQuery;
        private StringBuilder _hint;
        private List<int> _closedLetters;

        private ConcurrentQueue<Message> _messages = new ConcurrentQueue<Message>();

        public Quiz(IBotService bot, IUserService userService, IQueryService queryService, long _chatId)
        {
            _bot = bot;
            _userService = userService;
            _queryService = queryService;

            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += async (sender, e) => await ProcessMessages();
            _updateTimer.AutoReset = true;
            _updateTimer.Enabled = true;

            _hintTimer = new Timer(15000);
            _hintTimer.Elapsed += async (sender, e) => await UpdateHint();
            _hintTimer.AutoReset = true;
            _hintTimer.Enabled = true;
        }

        public async Task NextQuery()
        {
            var maxId = await _queryService.GetMaxId();
            var randomId = new Random().Next(1, maxId);
            var query = await _queryService.GetById(randomId);

            _hint = new StringBuilder(new string('*', query.Answer.Length));
            _closedLetters = Enumerable.Range(0, _hint.Length - 1).ToList();

            _currentQuery = query;
            await SendQuery();
            _updateTimer.Start();
            _hintTimer.Start();
        }

        public Task StopQuiz()
        {
            _updateTimer.Close();
            _hintTimer.Close();
            return Task.CompletedTask;
        }
        
        public Task SaveMessage(Message message)
        {
            _messages.Enqueue(message);
            return Task.CompletedTask;
        }

        private async Task ProcessMessages()
        {
            var messages = new List<Message>();
            while (_messages.TryDequeue(out var message))
            {
                messages.Add(message);
            }

            var sortedMessages = messages.OrderBy((x => x.Date));
            foreach (var m in sortedMessages)
            {
                if (await CheckAnswer(m)) return;
            }
        }

        private async Task<bool> CheckAnswer(Message message)
        {
            if (!message.Text.ToLower().Equals(_currentQuery.Answer))
                return false;
            
            await _bot.SendMessage(_chatId,
                "Верно ответил " + message.From.Username +
                "и получает " + _closedLetters.Count + " баллов!\n",  message.MessageId);

            _hintTimer.Stop();
            _updateTimer.Stop();
            
            await _userService.UpdateUserScore(
                message.From.Id,
                message.Chat.Id,
                _closedLetters.Count,
                message.From.Username);
            await NextQuery();
            
            return true;
        }

        private async Task UpdateHint()
        {
            if (_currentQuery == null)
            {
                await NextQuery();
            }

            if (_closedLetters.Count == 1)
            {
                await _bot.SendMessage(_chatId,
                    "Никто не дал правильного ответа. \n" +
                    "Правильный ответ: " + _currentQuery.Answer);
                
                await NextQuery();
            }
            else
            {
                var i = new Random().Next(_closedLetters.Count);
                var iol = _closedLetters[i];
                _hint[iol] = _currentQuery.Answer[iol];
                _closedLetters.Remove(iol);
            }

            await SendQuery();
        }

        private async Task SendQuery()
        {
            await _bot.SendMessage(_chatId,
                _currentQuery.Question + "\n" +
                _hint
                );
        }
    }
}