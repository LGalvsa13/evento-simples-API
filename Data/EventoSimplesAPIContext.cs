using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventoSimplesAPI.Models;
using SimplesEventoApi.Models;

namespace EventoSimplesAPI.Data
{
    public class EventoSimplesAPIContext : DbContext
    {
        public EventoSimplesAPIContext (DbContextOptions<EventoSimplesAPIContext> options)
            : base(options)
        {
        }

        public DbSet<EventoSimplesAPI.Models.Evento> Evento { get; set; } = default!;
        public DbSet<EventoSimplesAPI.Models.Ingresso> Ingresso { get; set; } = default!;
        public DbSet<EventoSimplesAPI.Models.Local> Local { get; set; } = default!;
        public DbSet<SimplesEventoApi.Models.Participante> Participante { get; set; } = default!;
    }
}
