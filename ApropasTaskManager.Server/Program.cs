using ApropasTaskManager.Server;
using ApropasTaskManager.Server.Models;
using ApropasTaskManager.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));

builder.Services.AddDbContext<ApplicationContext>( options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationContext>()
   .AddDefaultTokenProviders();

builder.Services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidIssuers = authOptions.Issuers,
                        ValidateAudience = true,
                        ValidAudiences = authOptions.Audiences,
                        ValidateLifetime = true,
                        IssuerSigningKey = authOptions.SymmetricSecurityKey,
                        ValidateIssuerSigningKey = true,
                    };
                });
builder.Services.AddAuthentication();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    var securityRequirement = new OpenApiSecurityRequirement
{
    {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityDefinition("bearerAuth", securityScheme);
    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//DataSeeder
var services = app.Services.CreateAsyncScope();
await DataSeeder.InitializeAsync(
    services.ServiceProvider.GetRequiredService<UserManager<User>>(),
    services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>()
);

app.Run();


