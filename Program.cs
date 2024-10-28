using EventPlanning.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var url = builder.Configuration["ClientUrl"];
//builder.Services.AddCors(opts => opts.AddPolicy("AllowClient", policy =>
//policy.WithOrigins($"{builder.Configuration["ClientUrl"]}")
//    .AllowAnyHeader()
//    .AllowAnyMethod()
//    ));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // указывает, будет ли валидироваться издатель при валидации токена
        ValidateIssuer = true,
        // строка, представляющая издателя
        ValidIssuer = AuthOptions.ISSUER,
        // будет ли валидироваться потребитель токена
        ValidateAudience = true,
        // установка потребителя токена
        ValidAudience = AuthOptions.AUDIENCE,
        // будет ли валидироваться время существования
        ValidateLifetime = true,
        // установка ключа безопасности
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        // валидация ключа безопасности
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddControllers();

builder.Services.AddDbContext<EventPlanningDbContext>(options => options.UseInMemoryDatabase("EventDb"));

//var connectionString = builder.Configuration.GetConnectionString("EventDb");

//if (connectionString != null && builder.Environment.IsDevelopment())
//{
//    builder.Services.AddDbContext<WeatheforecastDbContext>(options => options.UseSqlServer(connectionString));
//    using var scope = builder.Services.BuildServiceProvider().CreateScope();
//    var dbContext = scope.ServiceProvider.GetRequiredService<WeatheforecastDbContext>();
//    dbContext.Database.Migrate();
//}
//else
//{
//    builder.Services.AddDbContext<WeatheforecastDbContext>(options => options.UseInMemoryDatabase("EventDb"));
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    public const int LIFETIME = 20;
    const string KEY = "mysupersecret_secretsecretsecretkey!123";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}