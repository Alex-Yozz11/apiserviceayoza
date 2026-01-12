using api.service.factura.application.commons.dtos;
using api.service.factura.application.ifeatures;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.service.factura.presentation.endpoints;

public static class FacturaEndpoints
{
    public static RouteGroupBuilder MapFactura(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAll);
        builder.MapGet("/{id:int}", GetById);
        builder.MapPost("/", Insert);
        builder.MapPatch("/{id:int}", Update);
        builder.MapDelete("/{id:int}/{softDelete:int}", Delete);
        return builder;
    }

    static async Task<Results<Ok<List<FacturaResponseDto>>, ProblemHttpResult>> GetAll(IFacturaHandler facturaHandler)
    {
        return TypedResults.Ok(await facturaHandler.GetAll());
    }

    static async Task<Results<Ok<FacturaResponseDto>, NotFound<string>, ProblemHttpResult>> GetById(
        [FromRoute] int id,
        IFacturaHandler facturaHandler)
    {
        var factura = await facturaHandler.GetById(id);
        if (factura.FacturaId == 0)
        {
            return TypedResults.NotFound("No encontrado");
        }
        return TypedResults.Ok(factura);
    }

    static async Task<Results<Created<FacturaResponseDto>, ProblemHttpResult>> Insert(
        [FromBody] FacturaRequestDto facturaRequest,
        IFacturaHandler facturaHandler)
    {
        var factura = await facturaHandler.Insert(facturaRequest);
        return TypedResults.Created($"/v1/factura/{factura.FacturaId}", factura);
    }

    static async Task<Results<NoContent, NotFound<string>, ProblemHttpResult>> Update(
        [FromRoute] int id,
        [FromBody] FacturaRequestDto facturaRequest,
        IFacturaHandler facturaHandler)
    {
        var result = await facturaHandler.UpdateAsync(facturaRequest, id);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>, ProblemHttpResult>> Delete(
        [FromRoute] int id,
        [FromRoute] int softDelete,
        IFacturaHandler facturaHandler)
    {
        if (softDelete < 0 || softDelete > 1)
        {
            return TypedResults.BadRequest("El valor softDelete debe ser 0 o 1");
        }

        var result = await facturaHandler.Delete(id, softDelete == 1);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }
}