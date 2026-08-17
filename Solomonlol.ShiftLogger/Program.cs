using Microsoft.EntityFrameworkCore;
using ShiftLogger.Backend.Endpoints;
using ShiftLogger.Backend.Entities;
using ShiftLogger.Backend.Interfaces;
using ShiftLogger.Backend.Services;
using Solomonlol.ShiftLogger;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLServerConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(connectionString));
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDbService<User>, UserService>();
builder.Services.AddScoped<IDbService<Shift>, ShiftService>();




var app = builder.Build();


if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapUserEndpoints();
app.MapShiftEndpoints();

await app.RunAsync();