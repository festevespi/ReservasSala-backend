namespace MTR.AgendamentoSalas.API.Dtos;

public class ResponseDto<T>
{
    public List<string> Erros { get; set; } = new();
    public void AdicionaErro(string erro)
    {
        Erros?.Add(erro);
    }
    public bool PossuiErros() => Erros.Any();
    public T Dados { get; set; }
}
