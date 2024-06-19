
using API.Core;
using Application;
using DataAccess;
using Domain;
using Implementation;
using Implementation.Logging.UseCases;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var settings = new AppSettings();

            builder.Configuration.Bind(settings);

            builder.Services.AddSingleton(settings.Jwt);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<HotelHorizonContext>();

            builder.Services.AddTransient<JwtTokenCreator>();
            
            builder.Services.AddUseCases();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();

            builder.Services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();

            builder.Services.AddTransient<IExceptionLogger, DBExceptionLogger>();
            

            builder.Services.AddTransient<IApplicationActorProvider>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var request = accessor.HttpContext.Request;

                var authHeader = request.Headers.Authorization.ToString(); //moze i ["authorization]";

                var context = x.GetService<HotelHorizonContext>();
                var tokenStorage = x.GetService<ITokenStorage>();

                return new JwtApplicationActorProvider(authHeader, tokenStorage);
            });

            builder.Services.AddTransient(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                if (accessor.HttpContext == null)
                {
                    return new UnathorizedActor();
                }
                return x.GetService<IApplicationActorProvider>().GetActor();
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configuration =>
            {
                configuration.RequireHttpsMetadata = false;
                configuration.SaveToken = true;
                configuration.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Jwt.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                configuration.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        //Token dohvatamo iz Authorization header-a

                        Guid tokenId = context.HttpContext.Request.GetTokenId().Value;

                        var storage = builder.Services.BuildServiceProvider().GetService<ITokenStorage>();

                        if (!storage.Exists(tokenId))
                        {
                            context.Fail("Invalid token");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            var app = builder.Build();
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
