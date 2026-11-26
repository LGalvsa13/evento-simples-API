using Microsoft.EntityFrameworkCore;
using EventoSimplesAPI.Data;
using EventoSimplesAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace EventoSimplesAPI.Endpoint;

public static class IngressoEndpoints
{
    public static void MapIngressoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Ingresso").WithTags(nameof(Ingresso));

        group.MapGet("/", async (EventoSimplesAPIContext db) =>
        {
            return await db.Ingresso.ToListAsync();
        })
        .WithName("GetAllIngressos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Ingresso>, NotFound>> (int id, EventoSimplesAPIContext db) =>
        {
            return await db.Ingresso.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Ingresso model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetIngressoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Ingresso ingresso, EventoSimplesAPIContext db) =>
        {
            var affected = await db.Ingresso
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, ingresso.Id)
                    .SetProperty(m => m.Nome, ingresso.Nome)
                    .SetProperty(m => m.LocalId, ingresso.LocalId)
                    .SetProperty(m => m.Preco, ingresso.Preco)
                    .SetProperty(m => m.QuantidadeDisponivel, ingresso.QuantidadeDisponivel)
                    .SetProperty(m => m.DataVendaInicio, ingresso.DataVendaInicio)
                    .SetProperty(m => m.DataVendaFim, ingresso.DataVendaFim)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateIngresso")
        .WithOpenApi();

        group.MapPost("/", async (Ingresso ingresso, EventoSimplesAPIContext db) =>
        {
            db.Ingresso.Add(ingresso);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Ingresso/{ingresso.Id}",ingresso);
        })
        .WithName("CreateIngresso")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, EventoSimplesAPIContext db) =>
        {
            var affected = await db.Ingresso
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteIngresso")
        .WithOpenApi();
    }
}
