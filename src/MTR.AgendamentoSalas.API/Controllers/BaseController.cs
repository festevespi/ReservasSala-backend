using Microsoft.AspNetCore.Mvc;
using MTR.AgendamentoSalas.API.Dtos;

namespace MTR.AgendamentoSalas.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult RetornoCustomizado<T>(ResponseDto<T> dados)
    {
        if (dados.PossuiErros())
        {
            return BadRequest(dados);
        }

        if (dados is null)
        {
            return NotFound(dados);
        }

        return Ok(dados);
    }

    protected IActionResult RetornoCustomizadoSemConteudo()
    {
        return NoContent();
    }
}
