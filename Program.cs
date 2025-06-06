
using ChatAPI.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using TradingSystemApi;
using TradingSystemApi.Context;
using TradingSystemApi.Entities.BusinessEntities.Customer;
using TradingSystemApi.Entities.BusinessEntities.Seller;
using TradingSystemApi.Entities.Documents.Receipts;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Repositories;
using TradingSystemApi.Services;
using TradingSystemApi.Services.ScheduleService;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

    string[] connectionType = { "MacConnection", "BusinessConnection" };
    builder.Services.AddDbContext<TradingSystemDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(connectionType[1])));
    builder.Services.AddAutoMapper(typeof(Program).Assembly);

    builder.Services.AddHostedService<InitServiceProvider>();

    builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
    builder.Services.AddScoped<IInitRepository, InitRepository>();
    builder.Services.AddScoped<IStoreRepository, StoreRepository>();
    builder.Services.AddScoped<IAdressRepository, AdressRepository>();
    builder.Services.AddScoped<IBusinessEntityRepository<Customer>, BusinessEntityRepository<Customer>>();
    builder.Services.AddScoped<IBusinessEntityRepository<Seller>, BusinessEntityRepository<Seller>>();
    //builder.Services.AddScoped<ISellerRepository, SellerRepository>();
    builder.Services.AddScoped<ICashierRepository, CashierRepository>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    //builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IBarcodeRepository, BarcodeRepository>();
    builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
    builder.Services.AddScoped<ISessionRepository, SessionRepository>();
    builder.Services.AddScoped<IDocumentRepository<ReceiptSale>, DocumentRepository<ReceiptSale>>();
    builder.Services.AddScoped<IDocumentItemRepository<ReceiptSaleItem>, DocumentItemRepository<ReceiptSaleItem>>();


    builder.Services.AddScoped<IInitService, InitService>();
    builder.Services.AddScoped<IStoreService, StoreService>();
    builder.Services.AddScoped<IAdressService, AdressService>();
    builder.Services.AddScoped<IBusinessEntityService<Customer>, BusinessEntityService<Customer>>();
    builder.Services.AddScoped<IBusinessEntityService<Seller>, BusinessEntityService<Seller>>();
    //builder.Services.AddScoped<ISellerService, SellerService>();
    builder.Services.AddScoped<ICashierService, CashierService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    //builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IBarcodeService, BarcodeService>();
    builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
    builder.Services.AddScoped<ISessionService, SessionService>();
    builder.Services.AddScoped<IReceiptSaleService, ReceiptSaleService>();
    builder.Services.AddScoped<IReceiptSaleItemService, ReceiptSaleItemService>();


    builder.Services.AddHostedService<ScheduleService>();
    //builder.Services.AddHostedService(provider => provider.GetRequiredService<ScheduledTaskService>());
    //IReceiptSaleItemService
    //ReceiptSaleService

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
