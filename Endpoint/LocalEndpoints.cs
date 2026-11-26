using Microsoft.EntityFrameworkCore;
using EventoSimplesAPI.Data;
using EventoSimplesAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace EventoSimplesAPI.Endpoint;

public static class LocalEndpoints
{
    public static void MapLocalEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Local").WithTags(nameof(Local));

        group.MapGet("/", async (EventoSimplesAPIContext db) =>
        {
            return await db.Local.ToListAsync();
        })
        .WithName("GetAllLocals")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Local>, NotFound>> (int id, EventoSimplesAPIContext db) =>
        {
            return await db.Local.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Local model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetLocalById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Local local, EventoSimplesAPIContext db) =>
        {
            var affected = await db.Local
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, local.Id)
                    .SetProperty(m => m.Nome, local.Nome)
                    .SetProperty(m => m.Endereco, local.Endereco)
                    .SetProperty(m => m.CapacidadeMaxima, local.CapacidadeMaxima)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateLocal")
        .WithOpenApi();

        group.MapPost("/", async (Local local, EventoSimplesAPIContext db) =>
        {
            db.Local.Add(local);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Local/{local.Id}",local);
        })
        .WithName("CreateLocal")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, EventoSimplesAPIContext db) =>
        {
            var affected = await db.Local
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteLocal")
        .WithOpenApi();
    }
}
