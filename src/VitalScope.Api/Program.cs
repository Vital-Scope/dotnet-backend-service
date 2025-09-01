using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using VitalScope.Api.Extensions;
using VitalScope.Api.HostedServices;
using VitalScope.Common.Consts;
using VitalScope.Insfrastructure;
using VitalScope.Insfrastructure.Extensions;
using VitalScope.Logic.Extensions;
using VitalScope.Logic.Services.Study;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    
    builder.Configuration.AddEnvironmentVariables();
    builder.Configuration.AddJsonFile("appsettings.json", false, true);
    builder.Services.ConfigureOptionsConfigs(builder.Configuration);

    builder.Services.AddLogic();
    builder.Services.AddHostedService<MigrationHostedService>();

    builder.Services.AddDatabase<ApplicationDbContext>(CommonConsts.MigrationsHistoryTableName);
    builder.Services.AddInfrastructure();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddEndpointsApiExplorer();
    
    
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "VitalScope API", Version = "v1" });
    });
    
    builder.Services.AddHealthChecks();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });

    AppContext.SetSwitch(CommonConsts.TimestampMapping, true);
    
    var app = builder.Build();
    
    app.MapGet("/add-info", async (IStudyService service) =>
        await service.AddInformationsAsync())
        .WithName("info")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "info create",
            Description = "Returns information about all the available books from the Amy's library.",
            Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
        });
    
    app.MapGet("/id", async (Guid id, IStudyService service) =>
            await service.GetValuesByIdAsync(id))
        .WithName("info-id")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "info create",
            Description = "Returns information about all the available books from the Amy's library.",
            Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
        });
    
    app.MapGet("/all", async (IStudyService service) =>
            await service.GetMetaInformatiosAsync())
        .WithName("info-all")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "info create",
            Description = "Returns information about all the available books from the Amy's library.",
            Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
        });

    app.UseCors("AllowAll");
    app.UseRouting();
    
    app.UseSwagger(); 
    app.UseSwaggerUI();
    
    app.UseHttpsRedirection();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception exception) when
    (exception is not HostAbortedException && exception.Source != "Microsoft.EntityFrameworkCore.Design")
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

