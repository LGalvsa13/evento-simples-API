using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SimplesEventoApi.Models;

public class Participante
{
    [Key]
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [ForeignKey("Ingresso")]
    public int IngressoId { get; set; }
    public DateTimeOffset DataRegistro { get; set; } = DateTimeOffset.UtcNow;
    public string StatusPagamento { get; set; } = "Aprovado";
}
