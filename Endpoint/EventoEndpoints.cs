using Microsoft.EntityFrameworkCore;
using EventoSimplesAPI.Data;
using EventoSimplesAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace EventoSimplesAPI.Endpoint;

public static class EventoEndpoints
{
    public static void MapEventoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Evento").WithTags(nameof(Evento));

        group.MapGet("/", async (EventoSimplesAPIContext db) =>
        {
            return await db.Evento.ToListAsync();
        })
        .WithName("GetAllEventos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Evento>, NotFound>> (int id, EventoSimplesAPIContext db) =>
        {
            return await db.Evento.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Evento model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEventoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Evento evento, EventoSimplesAPIContext db) =>
        {
            var affected = await db.Evento
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, evento.Id)
                    .SetProperty(m => m.Titulo, evento.Titulo)
                    .SetProperty(m => m.Descricao, evento.Descricao)
                    .SetProperty(m => m.DataHorarioInicio, evento.DataHorarioInicio)
                    .SetProperty(m => m.DataHorarioFim, evento.DataHorarioFim)
                    .SetProperty(m => m.LocalId, evento.LocalId)
                    .SetProperty(m => m.Status, evento.Status)
                    .SetProperty(m => m.DataCriacao, evento.DataCriacao)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEvento")
        .WithOpenApi();

        group.MapPost("/", async (Evento evento, EventoSimplesAPIContext db) =>
        {
            db.Evento.Add(evento);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Evento/{evento.Id}",evento);
        })
        .WithName("CreateEvento")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, EventoSimplesAPIContext db) =>
        {
            var affected = await db.Evento
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEvento")
        .WithOpenApi();
    }
}
