
using ExoCrud.DevenirDev2.Controllers;
using ExoCrud.DevenirDev2.Data;
using ExoCrud.DevenirDev2.Repository.AuthServices;
using ExoCrud.DevenirDev2.Repository.CarServices;
using ExoCrud.DevenirDev2.Repository.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Net;
using System.Text;

namespace ExoCrud.DevenirDev2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Configuration Pour Entity Framework Core

            // Recuperation de la chaine de connexion du fichier appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("MyConnection");

            // Configuration de Entity Framework
            builder.Services.AddDbContext<ExoContext>(option =>
            option.UseSqlServer(connectionString)
            );
            


            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //1.AddSingleton :
            //Retourne toujours la même instance d’objet
                        //builder.Services.AddSingleton<>();

            //2.AddScoped :
            //Dans le même scope, retourne toujours la même instance d’objet
            //builder.Services.AddScoped<>();

            //3.AddTransient :
            //Retourne toujours une nouvelle instance d’objet
            //builder.Services.AddTransient<>();


            builder.Services.AddScoped<ICarService,CarService>();
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<IAuthService,AuthService>();

            // Configuration JWT 
            // Recuperation de la clé secrète du fichier appsettings.json
            var secretKey = builder.Configuration["Jwt:SecretKey"];

            // Configuration de l'authentification JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        
                    };
                }
                );




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
