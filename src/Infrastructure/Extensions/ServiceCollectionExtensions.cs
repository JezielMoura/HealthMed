using System.Diagnostics.CodeAnalysis;
using System.Text;
using HealthMed.Application.Abstractions;
using HealthMed.Infrastructure.Persistence.Context;
using HealthMed.Infrastructure.Persistence.Repositories;
using HealthMed.Infrastructure.Providers;
using HealthMed.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HealthMed.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<ISchedulingRepository, SchedulingRepository>();
    }

    public static void ConfigureEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(builder => 
            builder.UseNpgsql(configuration["ConnectionStrings:Postgres"], options => 
                options.EnableRetryOnFailure(5)));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
    }

    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Secret"] ?? throw new ArgumentException("Secret cannot be empty"));

        services
            .AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        services.AddAuthorization();

        services.AddScoped<ITokenProvider>((sp) => new TokenProvider(key));

        services.AddAuthorizationBuilder().AddPolicy("doctor", policy => policy.RequireRole("Doctor"));
        services.AddAuthorizationBuilder().AddPolicy("patient", policy => policy.RequireRole("Patient"));
    }

        public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthMed" });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
            { 
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            }); 

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {   
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMailService>((sp) => new MailService(configuration));
    }
}
