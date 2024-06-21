using CaseItau.Application.Services.Fundo;
using CaseItau.Application.Services.TipoFundo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IFundoService, FundoService>();
            services.AddScoped<ITipoFundoService, TipoFundoService>();

            return services;
        }
    }
}
