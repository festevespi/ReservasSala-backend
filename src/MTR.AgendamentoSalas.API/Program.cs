using Microsoft.EntityFrameworkCore;
using MTR.AgendamentoSalas.API.Data;
using MTR.AgendamentoSalas.API.Services;

var builder = WebApplication.CreateBuilder(args);

var stringConexao = builder.Configuration.GetConnectionString("banco-mysql");

builder.Services.AddDbContext<AppDbContexto>(options =>
    options.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao),
        mySqlOptions => mySqlOptions.EnableStringComparisonTranslations())
);

builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Agendamento de Salas API", Version = "v1" });
});

var POLITICA_CORS = "PermitirTudo";

builder.Services.AddCors(options =>
{
    options.AddPolicy(POLITICA_CORS, policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(POLITICA_CORS);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
