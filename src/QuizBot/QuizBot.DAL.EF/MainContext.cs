using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBot.DAL.Contracts;
using QuizBot.Entities;

namespace QuizBot.DAL.EF
{
    public class MainContext : DbContext, IMainContext
    {
        private string ConnectionString { get; set; }
        
        public DbSet<Query> Queries { get; set; }
        public DbSet<User> Users { get; set; }

        public MainContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        async Task IMainContext.SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}