using Microsoft.AspNetCore.Mvc;
using MTR.AgendamentoSalas.API.Models;
using MTR.AgendamentoSalas.API.Services;

namespace MTR.AgendamentoSalas.API.Controllers;

[Route("api/[controller]")]
public class ReservasController(IReservaService reservaService) : BaseController
{
    private readonly IReservaService _reservaService = reservaService;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var retorno = await _reservaService.ObterPorId(id);
        return RetornoCustomizado(retorno);
    }

    [HttpPost]
    public async Task<IActionResult> Inserir(Reserva reserva)
    {
        var retorno = await _reservaService.Inserir(reserva);

        return RetornoCustomizado(retorno);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, Reserva reserva)
    {
        var retorno = await _reservaService.Atualizar(id, reserva);

        return RetornoCustomizado(retorno);
    }

    [HttpGet("salas")]
    public async Task<IActionResult> ConsultarTodasSalas()
    {
        var retorno = await _reservaService.ObterTodasSalas();
        return RetornoCustomizado(retorno);
    }

    [HttpGet("locais")]
    public async Task<IActionResult> ConsultarTodosLocais()
    {
        var retorno = await _reservaService.ObterTodosLocais();
        return RetornoCustomizado(retorno);
    }
}
