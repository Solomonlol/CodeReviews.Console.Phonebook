using Microsoft.EntityFrameworkCore;
using Solomonlol.ShiftLogger;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLServerConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(connectionString));


var app = builder.Build();

