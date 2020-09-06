using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Configuration = configuration;
        }
        
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IMessageService, MessageService>();
            services.AddSingleton<IBotService, BotService>();

            services.AddTransient<IMainContext, MainContext>(contextProvider =>
                new MainContext(Configuration["BotConfiguration:DbConnectionString"]));
            services.AddTransient<IQueryService, QueryService>();
            services.AddTransient<IUserService, UserService>();

            services.Configure<BotConfig>(Configuration.GetSection("BotConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseCors();
            }
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}