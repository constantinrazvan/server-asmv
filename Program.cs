using Microsoft.EntityFrameworkCore;
using ServerAsmv.Data;
using ServerAsmv.Services;
using ServerAsmv.Utils;

var builder = WebApplication.CreateBuilder(args);

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettingsSection.GetValue<string>("SecretKey");
var issuer = jwtSettingsSection.GetValue<string>("Issuer");
var audience = jwtSettingsSection.GetValue<string>("Audience");

builder.Services.AddDbContext<AppData>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtUtil>(provider => new JwtUtil(secretKey!, issuer!, audience!));
builder.Services.AddScoped<BecomeVolunteerService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<ProjectsService>();
builder.Services.AddScoped<VolunteersService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();