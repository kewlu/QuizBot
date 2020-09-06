using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QuizBot.Entities;

namespace QuizBot.DAL.Contracts
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();
        
        Task<T> Get(int id);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        
        Task Create(T item);
        
        Task Update(T item);
        
        Task Delete(int id);
    }
}