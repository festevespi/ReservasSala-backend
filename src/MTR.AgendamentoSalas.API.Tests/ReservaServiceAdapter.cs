using MTR.AgendamentoSalas.API.Data;
using MTR.AgendamentoSalas.API.Dtos;
using MTR.AgendamentoSalas.API.Models;
using MTR.AgendamentoSalas.API.Services;

namespace MTR.AgendamentoSalas.API.Tests;

public class ReservaServiceAdapter(IReservaRepositorio repo) : IReservaService
{
    private readonly IReservaRepositorio _repo = repo;

    public async Task<ResponseDto<List<Reserva>>> Obter()
    {
        return new ResponseDto<List<Reserva>>
        {
            Dados = await _repo.Obter()
        };
    }

    public async Task<ResponseDto<Reserva>> ObterPorId(int id)
    {
        return new ResponseDto<Reserva>
        {
            Dados = await _repo.ObterPorId(id)
        };
    }

    public async Task<ResponseDto<Reserva>> Inserir(Reserva reserva)
    {
        var retorno = new ResponseDto<Reserva>();

        ValidarEntradas(reserva, retorno);

        retorno.Dados = await _repo.Inserir(reserva);
        return retorno;
    }

    public async Task<ResponseDto<Reserva>> Atualizar(int id, Reserva reserva)
    {
        var retorno = new ResponseDto<Reserva>();
        var reservaLocalizada = await _repo.ObterPorId(id);
        if (reservaLocalizada == null)
        {
            retorno.AdicionaErro($"A reserva com id {id} não foi localizada.");
            return retorno;
        }

        ValidarEntradas(reserva, retorno);

        retorno.Dados = await _repo.Atualizar(id, reserva);
        return retorno;
    }

    public async Task<ResponseDto<Reserva>> Excluir(int id)
    {
        var retorno = new ResponseDto<Reserva>();
        var reservaLocalizada = await _repo.ObterPorId(id);
        if (reservaLocalizada == null)
        {
            retorno.AdicionaErro($"A reserva com id {id} não foi localizada.");
            return retorno;
        }

        await _repo.Excluir(reservaLocalizada);
        return retorno;
    }

    private static void ValidarEntradas(Reserva reserva, ResponseDto<Reserva> retorno) {
        if (reserva.DataInicio >= reserva.DataFim)
        {
            retorno.AdicionaErro("Data de início deve ser anterior à data de fim.");
        }

        if (reserva.DataInicio < DateTime.Now)
        {
            retorno.AdicionaErro("Não é possível reservar para datas no passado.");
        }

        if (reserva.Cafe is true && reserva.QuantidadePessoas is null || reserva.QuantidadePessoas == 0)
        {
            retorno.AdicionaErro("Quando pedir café lembre-se de informar a quantidade de pessoas. ;-)");
        }
    }

    public async Task<ResponseDto<List<Local>>> ObterTodosLocais()
    {
        return new ResponseDto<List<Local>>
        {
            Dados = await _repo.ObterTodosLocais()
        };
    }

    public async Task<ResponseDto<List<Sala>>> ObterTodasSalas()
    {
        return new ResponseDto<List<Sala>>
        {
            Dados = await _repo.ObterTodasSalas()
        };
    }
}