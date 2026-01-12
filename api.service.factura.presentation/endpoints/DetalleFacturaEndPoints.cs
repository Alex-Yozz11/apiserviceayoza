using api.service.factura.application.commons.dtos;
using api.service.factura.application.ifeatures;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.service.factura.presentation.endpoints;

public static class DetalleFacturaEndpoints
{
    public static RouteGroupBuilder MapDetalleFactura(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAll);
        builder.MapGet("/{id:int}", GetById);
        builder.MapPost("/", Insert);
        builder.MapPatch("/{id:int}", Update);
        builder.MapDelete("/{id:int}/{softDelete:int}", Delete);
        return builder;
    }

    static async Task<Results<Ok<List<DetalleFacturaResponseDto>>, ProblemHttpResult>> GetAll(IDetalleFacturaHandler detalleHandler)
    {
        return TypedResults.Ok(await detalleHandler.GetAll());
    }

    static async Task<Results<Ok<DetalleFacturaResponseDto>, NotFound<string>, ProblemHttpResult>> GetById(
        [FromRoute] int id,
        IDetalleFacturaHandler detalleHandler)
    {
        var detalle = await detalleHandler.GetById(id);
        if (detalle.DetalleFacturaId == 0)
        {
            return TypedResults.NotFound("No encontrado");
        }
        return TypedResults.Ok(detalle);
    }

    static async Task<Results<Created<DetalleFacturaResponseDto>, ProblemHttpResult>> Insert(
        [FromBody] DetalleFacturaRequestDto detalleRequest,
        IDetalleFacturaHandler detalleHandler)
    {
        var detalle = await detalleHandler.Insert(detalleRequest);
        return TypedResults.Created($"/v1/detallefactura/{detalle.DetalleFacturaId}", detalle);
    }

    static async Task<Results<NoContent, NotFound<string>, ProblemHttpResult>> Update(
        [FromRoute] int id,
        [FromBody] DetalleFacturaRequestDto detalleRequest,
        IDetalleFacturaHandler detalleHandler)
    {
        var result = await detalleHandler.UpdateAsync(detalleRequest, id);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>, ProblemHttpResult>> Delete(
        [FromRoute] int id,
        [FromRoute] int softDelete,
        IDetalleFacturaHandler detalleHandler)
    {
        if (softDelete < 0 || softDelete > 1)
        {
            return TypedResults.BadRequest("El valor softDelete debe ser 0 o 1");
        }

        var result = await detalleHandler.Delete(id, softDelete == 1);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }
}