using EventPlanning.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var url = builder.Configuration["ClientUrl"];
//builder.Services.AddCors(opts => opts.AddPolicy("AllowClient", policy =>
//policy.WithOrigins($"{builder.Configuration["ClientUrl"]}")
//    .AllowAnyHeader()
//    .AllowAnyMethod()
//    ));

builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        // ���������, ����� �� �������������� �������� ��� ��������� ������
//        ValidateIssuer = true,
//        // ������, �������������� ��������
//        ValidIssuer = AuthOptions.ISSUER,
//        // ����� �� �������������� ����������� ������
//        ValidateAudience = true,
//        // ��������� ����������� ������
//        ValidAudience = AuthOptions.AUDIENCE,
//        // ����� �� �������������� ����� �������������
//        ValidateLifetime = true,
//        // ��������� ����� ������������
//        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//        // ��������� ����� ������������
//        ValidateIssuerSigningKey = true,
//    };
//});

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
