using Microsoft.EntityFrameworkCore;
using MTR.AgendamentoSalas.API.Models;

namespace MTR.AgendamentoSalas.API.Data;

public class AppDbContexto(DbContextOptions<AppDbContexto> opcoes) : DbContext(opcoes)
{
    public DbSet<Local> Locais { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Sala> Salas { get; set; }
}