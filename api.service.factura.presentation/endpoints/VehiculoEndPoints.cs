using api.service.factura.application.commons.dtos;
using api.service.factura.application.ifeatures;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.service.factura.presentation.endpoints;

public static class VehiculoEndpoints
{
    public static RouteGroupBuilder MapVehiculo(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAll);
        builder.MapGet("/{id:int}", GetById);
        builder.MapPost("/", Insert);
        builder.MapPatch("/{id:int}", Update);
        builder.MapDelete("/{id:int}/{softDelete:int}", Delete);
        return builder;
    }

    static async Task<Results<Ok<List<VehiculoResponseDto>>, ProblemHttpResult>> GetAll(IVehiculoHandler vehiculoHandler)
    {
        return TypedResults.Ok(await vehiculoHandler.GetAll());
    }

    static async Task<Results<Ok<VehiculoResponseDto>, NotFound<string>, ProblemHttpResult>> GetById(
        [FromRoute] int id,
        IVehiculoHandler vehiculoHandler)
    {
        var vehiculo = await vehiculoHandler.GetById(id);
        if (vehiculo.VehiculoId == 0)
        {
            return TypedResults.NotFound("No encontrado");
        }
        return TypedResults.Ok(vehiculo);
    }

    static async Task<Results<Created<VehiculoResponseDto>, ProblemHttpResult>> Insert(
        [FromBody] VehiculoRequestDto vehiculoRequest,
        IVehiculoHandler vehiculoHandler)
    {
        var vehiculo = await vehiculoHandler.Insert(vehiculoRequest);
        return TypedResults.Created($"/v1/vehiculo/{vehiculo.VehiculoId}", vehiculo);
    }

    static async Task<Results<NoContent, NotFound<string>, ProblemHttpResult>> Update(
        [FromRoute] int id,
        [FromBody] VehiculoRequestDto vehiculoRequest,
        IVehiculoHandler vehiculoHandler)
    {
        var result = await vehiculoHandler.UpdateAsync(vehiculoRequest, id);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>, ProblemHttpResult>> Delete(
        [FromRoute] int id,
        [FromRoute] int softDelete,
        IVehiculoHandler vehiculoHandler)
    {
        if (softDelete < 0 || softDelete > 1)
        {
            return TypedResults.BadRequest("El valor softDelete debe ser 0 o 1");
        }

        var result = await vehiculoHandler.Delete(id, softDelete == 1);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }
}