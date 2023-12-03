using Backend.Database;
using Backend.DbContexts;
using Backend.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp",
			builder => builder.WithOrigins("http://localhost:3000")
												.AllowAnyMethod()
												.AllowAnyHeader());
});

builder.Services.AddDbContext<SheetMusicManagementContext>(options =>
{
 options.UseNpgsql(builder.Configuration.GetConnectionString("SsheetMusicDatabase"))
#if DEBUG
 .LogTo(Console.WriteLine, LogLevel.Information)
 .EnableSensitiveDataLogging()
#endif
 ;
});

builder.Services.AddScoped<SheetMusicRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	// Configure method
	app.UseCors("AllowReactApp");
}


app.MapGet("/song", () =>
{
	var songs = Enumerable.Range(1, 3).Select(index =>
			new Song($"song nr {index}"))
			.ToArray();
	return songs;
})
.WithName("GetSong")
.WithOpenApi();

app.Run();
