using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using FluentValidation;
using FluentValidation.AspNetCore;
using QuestApi.Filters;
using QuestApi.Data;
using QuestApi.Mapping;

namespace QuestApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public IConfiguration Configuration => _configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(
            options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }
        )
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.WriteIndented = true;
        });

        // Add swagger
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Quest Project API", Version = "v1" });
            // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            // var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            // options.IncludeXmlComments(xmlFilePath);

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
        });

        services.AddEndpointsApiExplorer();

        // Add DB Connection string
        services.AddDbContext<QuestDbContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("questDevString") ?? throw new InvalidOperationException("Database Connection String Not Found...")).UseLazyLoadingProxies()
        );

        // Add Mappers
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Configure Cors
        services.AddCors(options => options.AddPolicy("corsPolicy", builder =>
        {
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));

        // Add AutoValidator
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        // Add Cashing
        services.AddResponseCaching();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        //app.UseLogResponseHttp();

        app.UseCors("corsPolicy");

        app.UseHttpsRedirection();

        // Configure the HTTP request pipeline.
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Quest Project API V1");
            options.RoutePrefix = string.Empty;
        });

        app.UseRouting();

        app.UseResponseCaching();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}