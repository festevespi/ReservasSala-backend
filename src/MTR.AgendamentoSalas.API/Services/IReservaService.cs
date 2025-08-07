using MTR.AgendamentoSalas.API.Dtos;
using MTR.AgendamentoSalas.API.Models;

namespace MTR.AgendamentoSalas.API.Services;

public interface IReservaService
{
    Task<ResponseDto<List<Reserva>>> Obter();
    Task<ResponseDto<Reserva>> ObterPorId(int id);
    Task<ResponseDto<Reserva>> Inserir(Reserva reserva);
    Task<ResponseDto<Reserva>> Atualizar(int id, Reserva reserva);
    Task<ResponseDto<Reserva>> Excluir(int id);

    Task<ResponseDto<List<Local>>> ObterTodosLocais();
    Task<ResponseDto<List<Sala>>> ObterTodasSalas();
}