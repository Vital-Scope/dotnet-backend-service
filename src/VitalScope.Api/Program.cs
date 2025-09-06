using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using VitalScope.Api.Extensions;
using VitalScope.Api.HostedServices;
using VitalScope.Common.Consts;
using VitalScope.Common.Options;
using VitalScope.Insfrastructure;
using VitalScope.Insfrastructure.Extensions;
using VitalScope.Insfrastructure.Identity;
using VitalScope.Insfrastructure.Identity.Authentication;
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
    builder.Services.AddDatabase<ApplicationIdentityDbContext>("__EFMigrationsIdentityHistoryApplication");
    
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

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
    
    var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions))
        .Get<JwtOptions>();

    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

   /* builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
        {
            opt.Password.RequireDigit = true;
        }).AddEntityFrameworkStores<ApplicationIdentityDbContext>()
        .AddDefaultTokenProviders();
    */
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = jwtOptions.ValidAudience,
            ValidIssuer = jwtOptions.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };
    });


    AppContext.SetSwitch(CommonConsts.TimestampMapping, true);
    
    var app = builder.Build();
    
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
    
    app.UseAuthentication();
    app.UseAuthorization();
    
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

