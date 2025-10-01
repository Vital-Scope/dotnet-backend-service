using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using VitalScope.Api.Conventions;
using VitalScope.Api.Extensions;
using VitalScope.Api.HostedServices;
using VitalScope.Common.Consts;
using VitalScope.Common.Options;
using VitalScope.Insfrastructure;
using VitalScope.Insfrastructure.Extensions;
using VitalScope.Insfrastructure.Identity;
using VitalScope.Insfrastructure.Identity.Authentication;
using VitalScope.Logic.Extensions;
using VitalScope.Logic.Hubs;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new LowerCaseControllerRouteConvention());
    }).AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
        
    
    builder.Configuration.AddEnvironmentVariables();
    builder.Configuration.AddJsonFile("appsettings.json", false, true);
    builder.Services.ConfigureOptionsConfigs(builder.Configuration);

    builder.Services.AddLogic();
    
    builder.Services.AddSignalR();
    
    builder.Services.AddHttpClient("NodeHttpClient", (s, client) =>
    {
        var options = s.GetRequiredService<IConfiguration>().GetSection("ExternalServiceOptions").Get<ExternalServiceOptions>();
        
        client.BaseAddress = new Uri(options.EmulatorHost);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });
    
    builder.Services.AddHttpClient("MLHttpClient", (s, client) =>
    {
        var options = s.GetRequiredService<IConfiguration>().GetSection("ExternalServiceOptions").Get<ExternalServiceOptions>();
        
        client.BaseAddress = new Uri(options.MlHost);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });
    
    builder.Services.AddHostedService<MigrationHostedService>();
    builder.Services.AddHostedService<MqttHostedService>();

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
                corsPolicyBuilder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true) // разрешает вообще любой Origin
                    .AllowCredentials();
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

    app.UseCors("AllowAll");
    app.UseRouting();

    app.MapHub<SensorHub>("/sensor-page");
    
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

