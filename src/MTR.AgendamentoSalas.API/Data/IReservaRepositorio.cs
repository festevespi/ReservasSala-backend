using MTR.AgendamentoSalas.API.Models;

namespace MTR.AgendamentoSalas.API.Data;

public interface IReservaRepositorio
{
    Task<List<Reserva>> Obter();
    Task<Reserva?> ObterPorId(int id);
    Task<Reserva?> Inserir(Reserva reserva);
    Task<Reserva?> Atualizar(int id, Reserva reserva);
    Task<bool> Excluir(Reserva reserva);
    
    Task<bool> ConsultaReservaConflitante(Reserva reserva, int id);
    Task<List<Local>> ObterTodosLocais();
    Task<List<Sala>> ObterTodasSalas();
}