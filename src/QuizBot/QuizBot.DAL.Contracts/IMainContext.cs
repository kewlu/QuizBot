using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBot.Entities;

namespace QuizBot.DAL.Contracts
{
    public interface IMainContext
    {
        DbSet<Query> Queries { get; set; }
        
        DbSet<User> Users { get; set; }

        Task SaveChangesAsync();
    }
}