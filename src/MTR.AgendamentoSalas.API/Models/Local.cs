using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTR.AgendamentoSalas.API.Models;

[Table("locais")]
public class Local : BaseModel
{

    [Column("nome")]
    [MaxLength(100)]
    public string Nome { get; set; }
}
