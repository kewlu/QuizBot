﻿using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBot.Entities;

namespace QuizBot.BLL.Contracts
{
    public interface IUserService
    {
        Task AddUser(User user);

        Task<User> GetById(int id);

        Task<User> GetByUserId(long userId);

        Task<IEnumerable<User>> GetByChatId(long chatId);
        
        Task<IEnumerable<User>> GetAll();

        Task UpdateUserScore(long userId, long chatId, int updScore, string name = null);
    }
}