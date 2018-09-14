using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VkBot.Manager.Data;
using VkBot.Manager.Helpers;
using VkBot.Manager.Services;
using VkBot.Manager.Services.Models;
using VkBot.Manager.Services.Telegram;
using VkBot.Manager.Services.Vk;

namespace VkBot.Manager
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _loggerFactory.AddAzureWebAppDiagnostics();
            _loggerFactory.AddConsole();
            _loggerFactory.AddDebug();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton<IVkConnection, VkConnection>();

            services.AddScoped<IVkGroupMessageService, VkGroupMessageService>();
            services.AddScoped<IVkGroupPhotoService, VkGroupPhotoService>();
            services.AddScoped<IStickerService, StickerService>();
            services.AddScoped<ITelegramService, TelegramService>();
            services.AddScoped<IConfigurationHelperService, ConfigurationHelperService>();
            services.AddScoped<IFileHelperService, FileHelperService>();
            services.AddScoped<IEmojiService, EmojiService>();
            services.AddScoped<IVkHelper, VkHelper>();
            services.AddScoped<IKeyboardService, KeyboardService>();
            services.AddScoped<IBotUserService, BotUserService>();
            services.AddScoped<IReceivedMessageService, ReceivedMessageService>();
            services.AddScoped<IAzureImageService, AzureImageService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=StickerSet}/{action=Index}/{id?}");
            });
        }
    }
}
