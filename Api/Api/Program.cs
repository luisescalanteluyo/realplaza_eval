using Api.Application.Services;
using Api.Domain.Interfaces;
using Api.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//agregado*****
//var constructor = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "IssuerDomain",
            ValidAudience = "IssuerDomain",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456890000000000asdfghjklñzxcvbnm"))
        };
    });



builder.Services.AddAuthorization();//agregado*****

// DI - repos & services
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// FluentValidation (si usas validators)
//builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("*", builder =>
//    {
//        builder.AllowAnyHeader()
//        .AllowAnyMethod()
//        .AllowCredentials();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()); // Permite cualquier origen
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("CorsPolicy");
app.UseCors(build => build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();//agregado*****
app.UseAuthorization();


// 2. Habilitar la política CORS
//app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
