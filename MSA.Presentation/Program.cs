using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MSA.Application.IRepositories;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Infrastructure;
using MSA.Infrastructure.Repositories;
using MSA.Infrastructure.Services;
using MSA.Presentation;
using MSA.Presentation.Configuration;
using MSA.Presentation.SeedData;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

// Add Repository to the container.
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();

//Add Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

// Add Configuration
builder.Configuration.SettingsBinding();

builder.Services.ConfigAddDbContext();

// Add services to the container.
builder.Services.AddRazorPages();

//Seed Data
builder.Services.SeedData().GetAwaiter().GetResult();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapRazorPages();

app.Run();
