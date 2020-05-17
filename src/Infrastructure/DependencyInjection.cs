﻿using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Infrastructure.Identity;
using tti_graduation_work.Infrastructure.Persistence;
using tti_graduation_work.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tti_graduation_work.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace tti_graduation_work.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("tti_graduation_workDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();

            services.AddTransient<IExternalAuthenticationService, ExternalAuthenticationService>();
            services.AddTransient<INotificationService, NotificationService>();

            services.AddTransient<IAuthRepository, AuthRepository>();

            return services;
        }
    }
}
