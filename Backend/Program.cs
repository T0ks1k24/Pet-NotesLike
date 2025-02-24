using Backend.Data;
using Backend.Infrastructure.Interface.IRepositories;
using Backend.Infrastructure.Interface.IServices;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

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

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<INoteService, NoteService>();
        builder.Services.AddScoped<INoteRepository, NoteRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(corsPolicy);

        app.MapControllers();

        app.Run();
    }
}
