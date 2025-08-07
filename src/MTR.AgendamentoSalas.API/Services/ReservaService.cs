using Microsoft.AspNetCore.Http.HttpResults;
using MTR.AgendamentoSalas.API.Data;
using MTR.AgendamentoSalas.API.Dtos;
using MTR.AgendamentoSalas.API.Models;

namespace MTR.AgendamentoSalas.API.Services;

public class ReservaService(IReservaRepositorio reservaRepositorio) : IReservaService
{
    private readonly IReservaRepositorio _reservaRepositorio = reservaRepositorio;

    private readonly string MENSAGEM_RESERVA = "O local {0} na sala {1} já possui reserva entre o periodo informado {2} e {3}";


    public async Task<ResponseDto<List<Reserva>>> Obter()
    {
        return new ResponseDto<List<Reserva>>
        {
            Dados = await _reservaRepositorio.Obter()
        };
    }

    public async Task<ResponseDto<Reserva>> ObterPorId(int id)
    {
        return new ResponseDto<Reserva>
        {
            Dados = await _reservaRepositorio.ObterPorId(id)
        };
    }


    public async Task<ResponseDto<Reserva>> Inserir(Reserva reserva)
    {
        var retorno = new ResponseDto<Reserva>();
        ValidarEntradas(reserva, retorno);

        if (retorno.Erros.Any())
        {
            return retorno;
        }

        if (await ValidarReserva(reserva))
        {
            retorno.AdicionaErro(string.Format(MENSAGEM_RESERVA, reserva.Local.Nome, reserva.Sala.Nome, reserva.DataInicio.ToString("dd/MM/yyyy HH:mm"), reserva.DataFim.ToString("HH:mm")));
            return retorno;
        }
        retorno.Dados = await _reservaRepositorio.Inserir(reserva);

        return retorno;
    }

    public async Task<ResponseDto<Reserva>> Atualizar(int id, Reserva reserva)
    {
        var retorno = new ResponseDto<Reserva>();
        ValidarEntradas(reserva, retorno);

        if (retorno.Erros.Any())
        {
            return retorno;
        }

        if (await ValidarReserva(reserva, id))
        {
            retorno.AdicionaErro(string.Format(MENSAGEM_RESERVA, reserva.Local.Nome, reserva.Sala.Nome, reserva.DataInicio.ToString("dd/MM/yyyy HH:mm"), reserva.DataFim.ToString("HH:mm")));
            return retorno;
        }

        var reservaLocalizada = await _reservaRepositorio.ObterPorId(id);
        if (reservaLocalizada == null)
        {
            retorno.AdicionaErro($"A reserva com id {id} não foi localizada.");
            return retorno;
        }
        reservaLocalizada.Local = reserva.Local;
        reservaLocalizada.Sala = reserva.Sala;
        reservaLocalizada.DataInicio = reserva.DataInicio;
        reservaLocalizada.DataFim = reserva.DataFim;
        reservaLocalizada.Responsavel = reserva.Responsavel;
        reservaLocalizada.Cafe = reserva.Cafe;
        reservaLocalizada.QuantidadePessoas = reserva.Cafe ? reserva.QuantidadePessoas : 0;
        reservaLocalizada.Descricao = reserva.Descricao;

        await _reservaRepositorio.Atualizar(id, reservaLocalizada);

        retorno.Dados = reservaLocalizada;

        return retorno;
    }

    public async Task<ResponseDto<Reserva>> Excluir(int id)
    {
        var retorno = new ResponseDto<Reserva>();

        var reservaLocalizada = await _reservaRepositorio.ObterPorId(id);
        if (reservaLocalizada == null)
        {
            retorno.AdicionaErro($"A reserva com id {id} não foi localizada.");
            return retorno;
        }

        await _reservaRepositorio.Excluir(reservaLocalizada);

        return retorno;
    }

    private static void ValidarEntradas(Reserva reserva, ResponseDto<Reserva> retorno)
    {
        if (reserva.DataInicio >= reserva.DataFim)
        {
            retorno.AdicionaErro("Data de início deve ser anterior à data de fim.");
        }

        if (reserva.DataInicio < DateTime.Now)
        {
            retorno.AdicionaErro("Não é possível reservar para datas no passado.");
        }

        if (reserva.Cafe is true && reserva.QuantidadePessoas is null && reserva.QuantidadePessoas == 0)
        {
            retorno.AdicionaErro("Quando pedir café lembre-se de informar a quantidade de pessoas. ;-)");
        }
    }

    private async Task<bool> ValidarReserva(Reserva reserva, int id = 0)
    {
        var reservasConflitantes = await _reservaRepositorio.ConsultaReservaConflitante(reserva, id);

        return reservasConflitantes;
    }

    public async Task<ResponseDto<List<Local>>> ObterTodosLocais()
    {
        var retorno = new ResponseDto<List<Local>>
        {
            Dados = await _reservaRepositorio.ObterTodosLocais()
        };
        return retorno;
    }

    public async Task<ResponseDto<List<Sala>>> ObterTodasSalas()
    {
        var retorno = new ResponseDto<List<Sala>>
        {
            Dados = await _reservaRepositorio.ObterTodasSalas()
        };
        return retorno;
    }
}