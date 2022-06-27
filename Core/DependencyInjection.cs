using Application.Common.Interfaces;
using Core.Identity;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<StoreDBContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<_10X_StagingContext>(options =>
                 options.UseSqlServer(
                     configuration.GetConnectionString("10xStaging"),
                     b => b.MigrationsAssembly(typeof(_10X_StagingContext).Assembly.FullName)));

                services.AddDbContext<StoreDBContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(StoreDBContext).Assembly.FullName)));

            }
            //For In-Memory Caching
            //services.AddMemoryCache();
            //For Redis Caching

            //services.AddScoped<IStoreDBContext>(provider => provider.GetRequiredService<StoreDBContext>());
            services.AddScoped<I10XStagingDbContext>(provider => provider.GetRequiredService<_10X_StagingContext>());

            services.AddScoped<IStoreDbContext>(provider => provider.GetRequiredService<StoreDBContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            //services
            //          .AddDefaultIdentity<ApplicationUser>()
            //          .AddRoles<IdentityRole>()
            //          .AddEntityFrameworkStores<StoreDBContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, StoreDBContext>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IRecordingUtilityService, RecordingUtilityService>();
            services.AddTransient<IAppSettings, AppSettingService>();
            services.AddTransient<ICommonFunctions, CommonFunctions>();
            // services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                });


            return services;
        }
    }
}
