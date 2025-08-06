using Microsoft.EntityFrameworkCore;
using MTR.AgendamentoSalas.API.Data;
using MTR.AgendamentoSalas.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var stringConexao = builder.Configuration.GetConnectionString("banco-mysql");

builder.Services.AddDbContext<AppDbContexto>(options =>
    options.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao),
        mySqlOptions => mySqlOptions.EnableStringComparisonTranslations())
);

builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<ReservaRepositorio>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
