using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ServerAsmv.Data;
using ServerAsmv.Helpers;
using ServerAsmv.Interfaces;
using ServerAsmv.Services;
using ServerAsmv.Utils;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog for logging

// Configure JWT settings
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettingsSection.GetValue<string>("SecretKey");
var issuer = jwtSettingsSection.GetValue<string>("Issuer");
var audience = jwtSettingsSection.GetValue<string>("Audience");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Append("Token-Expired", "true");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowAll", policy => policy.RequireAssertion(context => true));
});

builder.Services.AddDbContext<AppData>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configure CORS to allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cererile din orice origine
              .AllowAnyMethod()  // Permite toate metodele (GET, POST, PUT etc.)
              .AllowAnyHeader(); // Permite toate header-ele
    });
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtUtil>(provider => new JwtUtil(secretKey!, issuer!, audience!));
builder.Services.AddScoped<BecomeVolunteerService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<ProjectsService>();
builder.Services.AddScoped<VolunteerService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<PhotoService>();
builder.Services.AddScoped<ActivityService>();

// Configure Cloudinary settings
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure file upload limit
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // Limit to 100MB
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP port
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps(
            builder.Configuration["Kestrel:Endpoints:Https:Certificate:Path"], 
            builder.Configuration["Kestrel:Endpoints:Https:Certificate:KeyPath"]);
    });
});

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppData>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll"); // Apply CORS policy to allow all requests
app.UseAuthentication();
app.UseAuthorization();

// Map controllers to endpoints
app.MapControllers();

app.Run();

