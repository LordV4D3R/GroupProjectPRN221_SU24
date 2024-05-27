using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using MSA.Presentation.Extensions;

namespace MSA.Presentation.SeedData
{
    public static class ConfigDataSeed
    {
        public static async Task SeedData(this IServiceCollection services)
        {
            string path = "./SeedData/";

            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            IList<Account> accounts = null!;

            //Account
            if (!context.Accounts.Any())
            {
                accounts = FileExtension<Account>.LoadJson(path, "Account.json");
                await context.Accounts.AddRangeAsync(accounts);
                await context.SaveChangesAsync();
            }   
        }
    }
}
