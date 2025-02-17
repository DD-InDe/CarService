using System.Text;
using Api.Models.Database;
using Api.Models.Dtos;
using Api.Repositories;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<CarServiceDbContext>();



#region Repositories

builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();

#endregion

#region ModelServices

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IModelService<EmployeeDto, Employee>, EmployeeService>();

#endregion

#region Services

builder.Services.AddSingleton<IJwtTokenManager, JwtTokenManager>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

#endregion


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    String key = config.GetValue<String>("JwtConfig:Key")!;
    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
    options.SaveToken = true;
    options.TokenValidationParameters = new()
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();