using ATM.DataLayer.DbModel;
using ATM.Interfaces;
using ATM.DataLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ATM.Entities.Config;
using ATM.LogicLayer;
using ATM.LogicLayer.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ATM.LogicLayer.Mappers;

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
                .AddScoped<IMap<ATM.Entities.Card, ATM.DataLayer.DbModel.Card>, CardMapper>()
                .AddScoped<ICardsLogic, CardsLogic>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins(appSettings.Origin)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = configuration.GetValue<string>("JWT:Issuer"),
                    ValidateAudience = false,
                    ValidAudience = configuration.GetValue<string>("JWT:Audience"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:SigningKey")))
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