using Microsoft.EntityFrameworkCore;
using MTR.AgendamentoSalas.API.Models;

namespace MTR.AgendamentoSalas.API.Data;

public class ReservaRepositorio(AppDbContexto contextoDb)
{
    private readonly AppDbContexto ContextoDb = contextoDb;

    public async Task<Reserva?> Inserir(Reserva reserva)
    {
        ContextoDb.Salas.Attach(reserva.Sala);
        ContextoDb.Locais.Attach(reserva.Local);
        ContextoDb.Reservas.Add(reserva);
        await ContextoDb.SaveChangesAsync();
        return reserva;
    }

    public async Task<Reserva?> Atualizar(int id, Reserva reserva)
    {
        ContextoDb.Reservas.Update(reserva);
        await ContextoDb.SaveChangesAsync();
        return reserva;
    }

    public async Task<Reserva?> ObterPorId(int id)
    {
        return await ContextoDb.Reservas
            .AsNoTracking()
            .Include(r => r.Sala)
            .Include(r => r.Local)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Reserva>> ObterTodos()
    {
        return await ContextoDb.Reservas
            .AsNoTracking()
            .Include(r => r.Sala)
            .Include(r => r.Local)
            .ToListAsync();
    }

    public async Task<bool> ConsultaReservaConflitante(Reserva reserva, int id)
    {
        var reservasConflitantes = ContextoDb.Reservas
            .Where(r => r.Sala.Nome.Equals(reserva.Sala.Nome, StringComparison.CurrentCultureIgnoreCase)
                        && r.Local.Nome.Equals(reserva.Local.Nome, StringComparison.CurrentCultureIgnoreCase)
                        && ((reserva.DataInicio >= r.DataInicio && reserva.DataInicio < r.DataFim)     // Início da nova reserva está dentro de uma existente
                            || (reserva.DataFim > r.DataInicio && reserva.DataFim <= r.DataFim)        // Fim da nova reserva está dentro de uma existente
                            || (reserva.DataInicio <= r.DataInicio && reserva.DataFim >= r.DataFim))   // Nova reserva engloba uma existente completamente
                    );

        if (id != 0)
        {
            reservasConflitantes = reservasConflitantes
                .Where(r => r.Id != id);
        };

        var retornoValidacao = reservasConflitantes.Any();

        return retornoValidacao;
    }

    public async Task<bool> Excluir(Reserva reserva)
    {
        ContextoDb.Reservas.Remove(reserva);
        await ContextoDb.SaveChangesAsync();
        return true;
    }
}
