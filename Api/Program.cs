using System.Text;
using Api.Models.Database;
using Api.Models.Dtos;
using Api.Models.ViewModels;
using Api.Repositories;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddDbContext<CarServiceDbContext>();


#region Repositories

builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<ImportOrderRepository>();
builder.Services.AddScoped<MaterialRepository>();
builder.Services.AddScoped<OrderMaterialClientRepository>();
builder.Services.AddScoped<OrderMaterialServiceRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderServiceRepository>();
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<ServiceRepository>();
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<TransactionRepository>();

#endregion

#region ModelServices

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ClientService>();

#endregion

#region ControllerServices

builder.Services.AddSingleton<IJwtTokenManager, JwtTokenManager>();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<IAccountService, AccountService>();

#endregion

#region Jwt

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "Authorization",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    String key = config.GetValue<String>("JwtConfig:Key")!;
    String issuer = config.GetValue<String>("JwtConfig:Iss")!;
    String audience = config.GetValue<String>("JwtConfig:Aud")!;
    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
    options.TokenValidationParameters = new()
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateIssuerSigningKey = true
    };
});

#endregion

#region LogServices

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<LogService>();

#endregion

var app = builder.Build();
app.Logger.LogInformation("Сервер запущен");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

Log.CloseAndFlush();