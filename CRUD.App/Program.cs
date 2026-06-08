using CRUD.App.Application;
using CRUD.App.Infrastructure;
using CRUD.App.Middlewares;

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CORSPolicyName);

app.UseGlobalExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
