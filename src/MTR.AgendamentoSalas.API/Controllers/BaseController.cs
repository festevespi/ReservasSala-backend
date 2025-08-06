using Microsoft.AspNetCore.Mvc;
using MTR.AgendamentoSalas.API.Dtos;

namespace MTR.AgendamentoSalas.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    public IActionResult RetornoCustomizado<T>(ResponseDto<T> dados)
    {
        if (dados.PossuiErros())
        {
            return BadRequest(dados);
        }
        return Ok(dados);
    }
}
