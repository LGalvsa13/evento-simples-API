using Microsoft.EntityFrameworkCore;
using EventoSimplesAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using SimplesEventoApi.Models;
namespace EventoSimplesAPI.Endpoint;

public static class ParticipanteEndpoints
{
    public static void MapParticipanteEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Participante").WithTags(nameof(Participante));

        group.MapGet("/", async (EventoSimplesAPIContext db) =>
        {
            return await db.Participante.ToListAsync();
        })
        .WithName("GetAllParticipantes")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Participante>, NotFound>> (int id, EventoSimplesAPIContext db) =>
        {
            return await db.Participante.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Participante model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetParticipanteById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Participante participante, EventoSimplesAPIContext db) =>
        {
            var affected = await db.Participante
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, participante.Id)
                    .SetProperty(m => m.NomeCompleto, participante.NomeCompleto)
                    .SetProperty(m => m.Email, participante.Email)
                    .SetProperty(m => m.IngressoId, participante.IngressoId)
                    .SetProperty(m => m.DataRegistro, participante.DataRegistro)
                    .SetProperty(m => m.StatusPagamento, participante.StatusPagamento)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateParticipante")
        .WithOpenApi();

        group.MapPost("/", async (Participante participante, EventoSimplesAPIContext db) =>
        {
            db.Participante.Add(participante);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Participante/{participante.Id}",participante);
        })
        .WithName("CreateParticipante")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, EventoSimplesAPIContext db) =>
        {
            var affected = await db.Participante
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteParticipante")
        .WithOpenApi();
    }
}
