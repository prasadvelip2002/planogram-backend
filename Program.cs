using Microsoft.EntityFrameworkCore;
using PlanogramBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DataSeeder.Seed(context);
}

// Middleware
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

// ✅ FINAL FIX: Listen on all interfaces (so mobile can reach it)
app.Run("http://0.0.0.0:5272");
