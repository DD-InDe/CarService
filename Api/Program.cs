using System.Text;
using Api.Models.Database;
using Api.Models.Dtos;
using Api.Repositories;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

#region ControllerServices

builder.Services.AddSingleton<IJwtTokenManager, JwtTokenManager>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

#endregion

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

var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();