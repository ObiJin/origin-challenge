using ATM.DataLayer;
using ATM.DataLayer.DbModel;
using ATM.Entities.Config;
using ATM.Interfaces;
using ATM.LogicLayer;
using ATM.LogicLayer.Interfaces;
using ATM.LogicLayer.Mappers;
using ChallengeATM.ModelMappers;
using ChallengeATM.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChallengeATM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = builder.Configuration;

            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            var services = builder.Services;

            services.Configure<AppSettings>(appSettingsSection);

            services.AddDbContext<challengeATMContext>(options => options.UseSqlServer(configuration.GetConnectionString("ATMConnString")), ServiceLifetime.Transient);

            services.AddScoped<IRepository<ATM.DataLayer.DbModel.Card>, CardsRepository>()
                .AddScoped<IRepository<ATM.DataLayer.DbModel.Operation>, OperationsRepository>()
                .AddScoped<IMap<ATM.Entities.Card, ATM.DataLayer.DbModel.Card>, CardMapper>()
                .AddScoped<IMap<CardModel, ATM.Entities.Card>, CardModelMapper>()
                .AddScoped<IMap<ATM.Entities.Operation, ATM.DataLayer.DbModel.Operation>, OperationsMapper>()
                .AddScoped<ICardsLogic, CardsLogic>()
                .AddScoped<IOperationLogic, OperationsLogic>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins(appSettings.Origin)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var issuer = appSettings.Issuer;
            var audience = appSettings.Audience;

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = issuer,
                    ValidateAudience = false,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}