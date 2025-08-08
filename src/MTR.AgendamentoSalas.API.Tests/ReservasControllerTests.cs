using Moq;
using MTR.AgendamentoSalas.API.Data;
using MTR.AgendamentoSalas.API.Models;
using MTR.AgendamentoSalas.API.Services;

namespace MTR.AgendamentoSalas.API.Tests;



public class ReservasControllerTests
{
    private readonly Mock<IReservaRepositorio> _reservaRepositorioMock;
    private readonly IReservaService _reservaService;

    private readonly string RESPONSAVEL_CARLA = "Carla";
    private readonly string RESPONSAVEL_JOAO = "João";

    public ReservasControllerTests()
    {
        _reservaRepositorioMock = new Mock<IReservaRepositorio>();
        _reservaService = new ReservaServiceAdapter(_reservaRepositorioMock.Object);
    }

    [Fact]
    public async Task ObterPorId_DeveRetornarReserva_QuandoIdExiste()
    {
        // Arrange
        var reserva = new Reserva
        {
            Id = 1,
            Local = new Local { Id = 1, Nome = "Londrina" },
            Sala = new Sala { Id = 1, Nome = "Sala de Reunião 1" },
            DataInicio = DateTime.Now.AddHours(1),
            DataFim = DateTime.Now.AddHours(2),
            Responsavel = RESPONSAVEL_CARLA,
            Cafe = false
        };
        _reservaRepositorioMock.Setup(r => r.ObterPorId(1)).ReturnsAsync(reserva);

        // Act
        var result = await _reservaService.ObterPorId(1);

        // Assert
        Assert.NotNull(result.Dados);
        Assert.Equal(reserva.Id, result.Dados.Id);
    }

    [Fact]
    public async Task Inserir_DeveAdicionarCorretamente_CasoNaoExistaDados()
    {
        _reservaRepositorioMock.Setup(r => r.Obter()).ReturnsAsync([]);

        var reservas = await _reservaRepositorioMock.Object.Obter();

        if (!reservas.Any())
        {
            // Arrange
            var reserva = new Reserva
            {
                Local = new Local { Id = 0, Nome = "Londrina" },
                Sala = new Sala { Id = 0, Nome = "Sala de Reunião 1" },
                DataInicio = DateTime.Now.AddHours(2),
                DataFim = DateTime.Now.AddHours(4),
                Responsavel = RESPONSAVEL_CARLA,
                Cafe = false
            };

            // Act
            var result = await _reservaService.Inserir(reserva);

            // Assert
            Assert.False(result.PossuiErros());
            Assert.Empty(result.Erros);
            return;
        }
    }

    [Fact]
    public async Task Inserir_DeveOcorrerErro_QuandoDataInicioMaiorQueDataFim()
    {
        // Arrange
        var reserva = new Reserva
        {
            Local = new Local { Id = 1, Nome = "Londrina" },
            Sala = new Sala { Id = 1, Nome = "Sala de Reunião 1" },
            DataInicio = DateTime.Now.AddHours(2),
            DataFim = DateTime.Now.AddHours(1),
            Responsavel = RESPONSAVEL_CARLA,
            Cafe = false
        };

        // Act
        var result = await _reservaService.Inserir(reserva);

        // Assert
        Assert.True(result.PossuiErros());
        Assert.Contains("Data de início deve ser anterior à data de fim.", result.Erros);
    }

    [Fact]
    public async Task Inserir_DeveOcorrerErro_QuandoDataInicioInferiorDataHorarioAtual()
    {
        // Arrange
        var reserva = new Reserva
        {
            Local = new Local { Id = 1, Nome = "Londrina" },
            Sala = new Sala { Id = 1, Nome = "Sala de Reunião 1" },
            DataInicio = DateTime.Now.AddDays(-1),
            DataFim = DateTime.Now.AddDays(-1).AddHours(3),
            Responsavel = RESPONSAVEL_CARLA,
            Cafe = false
        };

        // Act
        var result = await _reservaService.Inserir(reserva);

        // Assert
        Assert.True(result.PossuiErros());
        Assert.Contains("Não é possível reservar para datas no passado.", result.Erros);
    }

    [Fact]
    public async Task Atualizar_DeveOcorrerErro_QuandoReservaNaoEncontrada()
    {
        // Arrange
        var reserva = new Reserva
        {
            Id = 99,
            Local = new Local { Id = 1, Nome = "Londrina" },
            Sala = new Sala { Id = 1, Nome = "Sala de Reunião 1" },
            DataInicio = DateTime.Now.AddHours(1),
            DataFim = DateTime.Now.AddHours(2),
            Responsavel = RESPONSAVEL_CARLA,
            Cafe = false
        };
        _reservaRepositorioMock.Setup(r => r.ObterPorId(99)).ReturnsAsync((Reserva?)null);

        // Act
        var result = await _reservaService.Atualizar(99, reserva);

        // Assert
        Assert.True(result.PossuiErros());
        Assert.Contains("A reserva com id 99 não foi localizada.", result.Erros);
    }

    [Fact]
    public async Task Atualizar_DeveOcorrerErro_QuandoServirCafeQuantidadeIgualZero()
    {
        // Arrange
        var reserva = new Reserva
        {
            Id = 1,
            Local = new Local { Id = 1, Nome = "Londrina" },
            Sala = new Sala { Id = 1, Nome = "Sala de Reunião 1" },
            DataInicio = DateTime.Now.AddHours(1),
            DataFim = DateTime.Now.AddHours(2),
            Responsavel = RESPONSAVEL_CARLA,
            Cafe = true
        };
        _reservaRepositorioMock.Setup(r => r.ObterPorId(1)).ReturnsAsync(reserva);

        // Act
        var result = await _reservaService.Atualizar(1, reserva);

        // Assert
        Assert.True(result.PossuiErros());
        Assert.Contains("Quando pedir café lembre-se de informar a quantidade de pessoas. ;-)", result.Erros);
    }

    [Fact]
    public async Task Excluir_DeveExcluirCorretamente()
    {
        // Arrange

        // Act
        var result = await _reservaService.Excluir(1);

        // Assert
        Assert.False(result.PossuiErros());
    }

    [Fact]
    public async Task Excluir_DeveOcorrerErro_ReservaNaoLocalizada()
    {
        // Arrange
        var id = 99;

        // Act
        var result = await _reservaService.Excluir(id);

        // Assert
        Assert.True(result.PossuiErros());
        Assert.Contains($"A reserva com id {id} não foi localizada.", result.Erros);
    }
}
