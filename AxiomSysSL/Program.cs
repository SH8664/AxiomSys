using AxiomSysBL.IServices.IAuthServices;
using AxiomSysBL.Services.AuthServices;
using AxiomSysDL.Models.SupplyChain;
using AxiomSysSL.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SupplyChainContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("SupplyChain")));


builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.TokenValidationParameters =
        new TokenValidationParameters
        {

            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ClockSkew = TimeSpan.Zero,


            ValidIssuer =
                builder.Configuration["jwt:issuer"],


            ValidAudience =
                builder.Configuration["jwt:audience"],


            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["jwt:key"]!
                    ))

        };
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AuthProfile>();
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SupplyChainContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
