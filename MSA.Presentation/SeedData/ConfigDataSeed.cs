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
            IList<Category> categorys = null!;
            IList<Product> products = null!;
            IList<Order> orders = null!;
            IList<OrderDetail> orderDetails = null!;



            //Account
            //if (!context.Accounts.Any())
            //{
            //    accounts = FileExtension<Account>.LoadJson(path, "ACCOUNT.json");
            //    await context.Accounts.AddRangeAsync(accounts);
            //    await context.SaveChangesAsync();
            //}





            //OrderDetail
            if (!context.OrderDetails.Any())
            {
                //Order
                if (!context.Orders.Any())
                {
                    //Account
                    if (!context.Accounts.Any())
                    {
                        accounts = FileExtension<Account>.LoadJson(path, "ACCOUNT.json");
                        await context.Accounts.AddRangeAsync(accounts);
                        await context.SaveChangesAsync();
                    }
                    orders = FileExtension<Order>.LoadJson(path, "ORDER.json");
                    await context.Orders.AddRangeAsync(orders);
                    await context.SaveChangesAsync();
                }
                //Product
                if (!context.Products.Any())
                {
                    //Category
                    if (!context.Categorys.Any())
                    {
                        categorys = FileExtension<Category>.LoadJson(path, "CATEGORY.json");
                        await context.Categorys.AddRangeAsync(categorys);
                        await context.SaveChangesAsync();
                    }
                    products = FileExtension<Product>.LoadJson(path, "PRODUCT.json");
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
                orderDetails = FileExtension<OrderDetail>.LoadJson(path, "ORDERDETAIL.json");
                await context.OrderDetails.AddRangeAsync(orderDetails);
                await context.SaveChangesAsync();

            }




            //Category&Product
            //if (!context.Products.Any())
            //{
            //    if (!context.Categorys.Any())
            //    {
            //        categorys = FileExtension<Category>.LoadJson(path, "CATEGORY.json");
            //        await context.Categorys.AddRangeAsync(categorys);
            //        await context.SaveChangesAsync();
            //    }
            //    products = FileExtension<Product>.LoadJson(path, "PRODUCT.json");
            //    await context.Products.AddRangeAsync(products);
            //    await context.SaveChangesAsync();
            //}


            //Category
            //if (!context.Categorys.Any())
            //{
            //    categorys = FileExtension<Category>.LoadJson(path, "CATEGORY.json");
            //    await context.Categorys.AddRangeAsync(categorys);
            //    await context.SaveChangesAsync();
            //}
            //Product
            //if (!context.Products.Any())
            //{
            //    products = FileExtension<Product>.LoadJson(path, "PRODUCT.json");
            //   await context.Products.AddRangeAsync(products);
            //    await context.SaveChangesAsync();
            //}
        }
    }
}
