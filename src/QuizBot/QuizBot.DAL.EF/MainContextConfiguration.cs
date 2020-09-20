using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QuizBot.DAL.EF
{
    public static class MainContextConfiguration
    {
        public static IServiceCollection AddMainContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<MainContext>(
                opt => opt.UseSqlServer(configuration["ConnectionString"], 
                    builder => builder.EnableRetryOnFailure(
                        maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null) ),
                ServiceLifetime.Transient);
        }
        
    }
}