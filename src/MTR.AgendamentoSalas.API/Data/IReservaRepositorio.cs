using MTR.AgendamentoSalas.API.Models;

namespace MTR.AgendamentoSalas.API.Data;

public interface IReservaRepositorio
{
    Task<Reserva?> Atualizar(int id, Reserva reserva);
    Task<bool> ConsultaReservaConflitante(Reserva reserva, int id);
    Task<bool> Excluir(Reserva reserva);
    Task<Reserva?> Inserir(Reserva reserva);
    Task<Reserva?> ObterPorId(int id);
    Task<List<Reserva>> ObterTodos();
    Task<List<Local>> ObterTodosLocais();
    Task<List<Sala>> ObterTodasSalas();
}