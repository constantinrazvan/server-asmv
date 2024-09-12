using Microsoft.EntityFrameworkCore;
using ServerAsmv.Data;
using ServerAsmv.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppData>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
);

builder.Services.AddScoped<AuthService>();
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