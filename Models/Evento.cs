using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventoSimplesAPI.Models;

public class Evento
{
    [Key]
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTimeOffset DataHorarioInicio { get; set; }
    public DateTimeOffset DataHorarioFim { get; set; }


    [ForeignKey("Local")]
    public int LocalId { get; set; }


    public string Status { get; set; } = "aberto";
    public DateTimeOffset DataCriacao { get; set; } = DateTimeOffset.UtcNow;
}

