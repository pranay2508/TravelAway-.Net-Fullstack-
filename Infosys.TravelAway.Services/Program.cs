using Infosys.TravelAway.DAL;
using Infosys.TravelAway.DAL.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<RentalSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository
builder.Services.AddScoped<RentalSystemRepository>();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
