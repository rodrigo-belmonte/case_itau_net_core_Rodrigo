using CaseItau.Application.Services.Fundo;
using CaseItau.Domain.Repository;
using CaseItau.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data
{
    public static class DataServiceRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<CaseItauContext>();
            services.AddScoped<IFundoRepository, FundoRepository>();
            services.AddScoped<ITipoFundoRepository, TipoFundoRepository>();

            return services;
        }
    }
}
