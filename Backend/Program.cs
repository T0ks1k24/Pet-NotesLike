using Backend.Data;
using Backend.Infrastructure.Interface.IRepositories;
using Backend.Infrastructure.Interface.IServices;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var corsPolicy = "_myAllowSpecificOrigins";

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.Services.AddScoped<JwtService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: corsPolicy,
            policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                ValidAudience = builder.Configuration["JwtOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Enter your JWT Access Token",
                Reference= new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            opt.AddSecurityDefinition("Bearer", jwtSecurityScheme);
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {jwtSecurityScheme, Array.Empty<string>() }
            });
        });

        builder.Services.AddScoped<INoteService, NoteService>();
        builder.Services.AddScoped<INoteRepository, NoteRepository>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<INoteListService, NoteListService>();
        builder.Services.AddScoped<INoteListRepository, NoteListRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(corsPolicy);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
