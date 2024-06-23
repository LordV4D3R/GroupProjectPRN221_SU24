using Autofac;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Common;
using MSA.Infrastructure;
using System.Data;

namespace MSA.Presentation.Configuration
{
    public static class ConfigureDbContext
    {
        
        public static IServiceCollection ConfigAddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(AppConfig.ConnectionStrings.DefaultConnection));
            return services;
        }

        public static ContainerBuilder AddDbContext(this ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().As<DbContext>().InstancePerLifetimeScope();
            return builder;
        }
    }
}
