using api.service.factura.application.commons.dtos;
using api.service.factura.application.ifeatures;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.service.factura.presentation.endpoints;

public static class VendedorEndpoints
{
    public static RouteGroupBuilder MapVendedor(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAll);
        builder.MapGet("/{id:int}", GetById);
        builder.MapPost("/", Insert);
        builder.MapPatch("/{id:int}", Update);
        builder.MapDelete("/{id:int}/{softDelete:int}", Delete);
        return builder;
    }

    static async Task<Results<Ok<List<VendedorResponseDto>>, ProblemHttpResult>> GetAll(IVendedorHandler vendedorHandler)
    {
        return TypedResults.Ok(await vendedorHandler.GetAll());
    }

    static async Task<Results<Ok<VendedorResponseDto>, NotFound<string>, ProblemHttpResult>> GetById(
        [FromRoute] int id,
        IVendedorHandler vendedorHandler)
    {
        var vendedor = await vendedorHandler.GetById(id);
        if (vendedor.VendedorId == 0)
        {
            return TypedResults.NotFound("No encontrado");
        }
        return TypedResults.Ok(vendedor);
    }

    static async Task<Results<Created<VendedorResponseDto>, ProblemHttpResult>> Insert(
        [FromBody] VendedorRequestDto vendedorRequest,
        IVendedorHandler vendedorHandler)
    {
        var vendedor = await vendedorHandler.Insert(vendedorRequest);
        return TypedResults.Created($"/v1/vendedor/{vendedor.VendedorId}", vendedor);
    }

    static async Task<Results<NoContent, NotFound<string>, ProblemHttpResult>> Update(
        [FromRoute] int id,
        [FromBody] VendedorRequestDto vendedorRequest,
        IVendedorHandler vendedorHandler)
    {
        var result = await vendedorHandler.UpdateAsync(vendedorRequest, id);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>, ProblemHttpResult>> Delete(
        [FromRoute] int id,
        [FromRoute] int softDelete,
        IVendedorHandler vendedorHandler)
    {
        if (softDelete < 0 || softDelete > 1)
        {
            return TypedResults.BadRequest("El valor softDelete debe ser 0 o 1");
        }

        var result = await vendedorHandler.Delete(id, softDelete == 1);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }
}