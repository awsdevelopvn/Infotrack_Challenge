using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SettlementBookingSystem.Application.Logging;

namespace SettlementBookingSystem.Application.Features
{
    public static class ServicesRegistration
    {
       public static void AddPersistenceServices(this IServiceCollection services)
       {
            // Example for custom mapping
            // MapsterProfile.Config();
           //TypeAdapterConfig<Domain.Company, SimpleCompanyResponse>
           //    .NewConfig()
           //    .Map(dest => dest.Id,
           //        src => src.Id.ToString());
           
           
            services.AddScoped<ILogTrace, LogTrace>();
            services.AddScoped<ILogger, ConsoleLogger>();
            // services.AddScoped<ITokenService, TokenService>();
            // services.AddScoped<ICommonService, CommonService>();
            var assembly1 = AppDomain.CurrentDomain.Load("SettlementBookingSystem.Application");
            services.AddMediatR(assembly1);
       }
    }
}