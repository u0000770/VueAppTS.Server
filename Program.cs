using Microsoft.EntityFrameworkCore;
using VueAppTS.Server.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container
builder.Services.AddDbContext<ThAmCo.Events.Model.EventsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CateringDatabase"))
);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<EventsController>();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add CORS policy
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
