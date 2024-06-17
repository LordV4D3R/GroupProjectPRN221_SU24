using Autofac;
using Microsoft.EntityFrameworkCore;
using MSA.Infrastructure;

namespace MSA.Presentation.Configuration
{
    public static class ConfigureDbContext
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=(local);Database=MSA;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            });
            return services;
        }
        public static ContainerBuilder AddDbContext(this ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
            return builder;
        }
    }
}
