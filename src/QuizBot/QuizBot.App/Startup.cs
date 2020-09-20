using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizBot.BLL.Contracts;
using QuizBot.BLL.Core.Models;
using QuizBot.BLL.Core.Services;
using QuizBot.DAL.Contracts;
using QuizBot.DAL.EF;

namespace QuizBot.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddMainContext(_configuration);
            //services.AddTransient<IMainContext, MainContext>(
            //    provider => new MainContext(_configuration["ConnectionString"]));
            services.AddTransient<IQueryService, QueryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<IQuizService, QuizService>();

            services.Configure<BotConfig>(_configuration.GetSection("BotConfiguration"));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseRouting();
            app.UseCors();
            
            app.UseEndpoints(endpoints =>endpoints.MapControllers());
        }
    }
}