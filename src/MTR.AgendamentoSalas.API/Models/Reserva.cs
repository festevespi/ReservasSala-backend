using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MTR.AgendamentoSalas.API.Models;

[Table("reservas")]
public class Reserva : BaseModel
{
    public Local Local { get; set; }
    public Sala Sala { get; set; }

    [Column("data_inicio")]
    [JsonPropertyName("data_inicio")]
    public DateTime DataInicio { get; set; }
    
    [Column("data_fim")]
    [JsonPropertyName("data_fim")]
    public DateTime DataFim { get; set; }
    
    [Column("responsavel")]
    public string Responsavel { get; set; }
    
    [Column("servir_cafe")]
    [JsonPropertyName("servir_cafe")]
    public bool Cafe { get; set; }
    
    [Column("quantidade_pessoas")]
    [JsonPropertyName("quantidade_pessoas")]
    public int? QuantidadePessoas { get; set; }
    
    [Column("descricao")]
    [MaxLength(500)]
    public string? Descricao { get; set; }
}
