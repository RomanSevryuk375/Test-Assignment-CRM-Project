using CRUD.App.Application;
using CRUD.App.Infrastructure;
using CRUD.App.Infrastructure.Persistence;
using CRUD.App.Middlewares;
using Microsoft.EntityFrameworkCore;

const string CORSPolicyName = "AllowFrontendSpa";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(CORSPolicyName, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CORSPolicyName);

app.UseGlobalExceptionHandler();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
