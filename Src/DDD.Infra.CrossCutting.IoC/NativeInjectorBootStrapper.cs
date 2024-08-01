using DDD.Application.Interfaces;
using DDD.Application.Services;
using DDD.Domain.Core.Notifications;
using DDD.Domain.Interfaces;
using DDD.Domain.Providers.Hubs;
using DDD.Domain.Providers.Webhooks;
using DDD.Domain.Providers.Http;
using DDD.Domain.Providers.Mail;
using DDD.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using DDD.Domain.Providers.Crons;

namespace DDD.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddHttpContextAccessor();
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)

            // ASP.NET Authorization Polices

            // Application
            services.AddScoped<ICDBAppService, CDBAppService>();

            // Event Source

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands

            // Domain - 3rd parties
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMailService, MailService>();

            // Domain - Providers
            services.AddScoped<IWebhookProvider, WebhookProvider>();
            services.AddScoped<ICronProvider, CronProvider>();

            // Infra - Data

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data EventSourcing

            // Infra - Identity Services

            // Infra - Identity
        }
    }
}
