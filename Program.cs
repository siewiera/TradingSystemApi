
using ChatAPI.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using TradingSystemApi;
using TradingSystemApi.Context;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Repositories;
using TradingSystemApi.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    string[] connectionType = { "MacConnection", "BusinessConnection" };
    builder.Services.AddDbContext<TradingSystemDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(connectionType[0])));
    builder.Services.AddAutoMapper(typeof(Program).Assembly);

    builder.Services.AddHostedService<InitServiceProvider>();

    builder.Services.AddScoped<IInitRepository, InitRepository>();
    builder.Services.AddScoped<IStoreRepository, StoreRepository>();
    builder.Services.AddScoped<IAdressRepository, AdressRepository>();
    builder.Services.AddScoped<ISellerRepository, SellerRepository>();
    builder.Services.AddScoped<ICashierRepository, CashierRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IBarcodeRepository, BarcodeRepository>();


    builder.Services.AddScoped<IInitService, InitService>();
    builder.Services.AddScoped<IStoreService, StoreService>();
    builder.Services.AddScoped<IAdressService, AdressService>();
    builder.Services.AddScoped<ISellerService, SellerService>();
    builder.Services.AddScoped<ICashierService, CashierService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IBarcodeService, BarcodeService>();


    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<BCryptHash>();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSignalR();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlingMiddleware>();


    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{ 
    NLog.LogManager.Shutdown();
}
