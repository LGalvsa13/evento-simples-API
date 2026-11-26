using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventoSimplesAPI.Models;

public class Ingresso
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    [ForeignKey("Evento")]
    public int LocalId { get; set; }
    public decimal Preco { get; set; }
    public int QuantidadeDisponivel { get; set; }
    public DateTimeOffset DataVendaInicio { get; set; }
    public DateTimeOffset DataVendaFim { get; set; }
}
