using EpicRoadTrip.Api.Configurations.Exceptions;
using EpicRoadTrip.Application;
using EpicRoadTrip.Application.Options;
using EpicRoadTrip.Infrastructure;
using EpicRoadTrip.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace EpicRoadTrip.Api.Configurations;

public static class Container
{
    public static WebApplication Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        RegisterServices(builder.Services, builder.Configuration);

        var app = builder.Build();
        ConfigureApplication(app);

        return app;
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Authorization using Bearer scheme",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        var issuer = configuration.GetSection(nameof(Jwt))[nameof(Jwt.Issuer)];
        var audience = configuration.GetSection(nameof(Jwt))[nameof(Jwt.Audience)];
        var key = configuration.GetSection(nameof(Jwt))[nameof(Jwt.Key)];
        if (string.IsNullOrEmpty(issuer) ||
            string.IsNullOrEmpty(audience) ||
            string.IsNullOrEmpty(key))
        {
            throw new AppsettingsJwtSectionIsNullException();
        }

        services.AddCors(options =>
        {
            options.AddPolicy(name: "frontEndBypass",
                              builder =>
                              {
                                  builder.WithOrigins("http://127.0.0.1:5173", "https://127.0.0.1:5173",
                                                    "http://localhost:5173", "https://localhost:5173")
                                          .AllowAnyMethod()
                                          .AllowAnyHeader();
                              });
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddApplication();
        services.AddInfrastructure(configuration);
    }

    private static void ConfigureApplication(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<EpicRoadTripContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("frontEndBypass");

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}