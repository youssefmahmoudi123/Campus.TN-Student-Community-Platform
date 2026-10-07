using Campus.Api.Data;
using Campus.Api.Models;
using Campus.Api.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using System.Text;


// ============================================================
// APPLICATION STARTUP
// ============================================================

var builder = WebApplication.CreateBuilder(args);


// ============================================================
// 1. DATABASE
// ============================================================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// ============================================================
// 2. APPLICATION SERVICES
// ============================================================

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


// ============================================================
// 3. JWT AUTHENTICATION
// ============================================================

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!)),

            ValidateIssuer = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,

            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };
    });


// ============================================================
// 4. CONTROLLERS
// ============================================================

builder.Services.AddControllers();


// ============================================================
// 5. SWAGGER / OPENAPI
// ============================================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Tell Swagger that we use JWT Bearer authentication.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Type = SecuritySchemeType.Http,

        Scheme = "bearer",

        BearerFormat = "JWT",

        In = ParameterLocation.Header,

        Description = "Enter your JWT token."
    });


    // Tell Swagger to use the Bearer scheme.
    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});


// ============================================================
// BUILD APPLICATION
// ============================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();


// ============================================================
// 6. SWAGGER UI
// ============================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// ============================================================
// 7. HTTPS
// ============================================================

app.UseHttpsRedirection();

app.UseCors("Frontend");


// ============================================================
// 8. AUTHENTICATION
// ============================================================

app.UseAuthentication();


// ============================================================
// 9. AUTHORIZATION
// ============================================================

app.UseAuthorization();


// ============================================================
// 10. CONTROLLERS
// ============================================================

app.MapControllers();


// ============================================================
// START APPLICATION
// ============================================================

app.Run();