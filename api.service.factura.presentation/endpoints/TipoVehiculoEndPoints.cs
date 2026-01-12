using api.service.factura.application.commons.dtos;
using api.service.factura.application.ifeatures;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.service.factura.presentation.endpoints;

public static class TipoVehiculoEndpoints
{
    public static RouteGroupBuilder MapTipoVehiculo(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAll);
        builder.MapGet("/{id:int}", GetById);
        builder.MapPost("/", Insert);
        builder.MapPatch("/{id:int}", Update);
        builder.MapDelete("/{id:int}/{softDelete:int}", Delete);
        return builder;
    }

    static async Task<Results<Ok<List<TipoVehiculoResponseDto>>, ProblemHttpResult>> GetAll(ITipoVehiculoHandler tipoVehiculoHandler)
    {
        return TypedResults.Ok(await tipoVehiculoHandler.GetAll());
    }

    static async Task<Results<Ok<TipoVehiculoResponseDto>, NotFound<string>, ProblemHttpResult>> GetById(
        [FromRoute] int id,
        ITipoVehiculoHandler tipoVehiculoHandler)
    {
        var tipoVehiculo = await tipoVehiculoHandler.GetById(id);
        if (tipoVehiculo.TipoVehiculoId == 0)
        {
            return TypedResults.NotFound("No encontrado");
        }
        return TypedResults.Ok(tipoVehiculo);
    }

    static async Task<Results<Created<TipoVehiculoResponseDto>, ProblemHttpResult>> Insert(
        [FromBody] TipoVehiculoRequestDto tipoVehiculoRequest,
        ITipoVehiculoHandler tipoVehiculoHandler)
    {
        var tipoVehiculo = await tipoVehiculoHandler.Insert(tipoVehiculoRequest);
        return TypedResults.Created($"/v1/tipovehiculo/{tipoVehiculo.TipoVehiculoId}", tipoVehiculo);
    }

    static async Task<Results<NoContent, NotFound<string>, ProblemHttpResult>> Update(
        [FromRoute] int id,
        [FromBody] TipoVehiculoRequestDto tipoVehiculoRequest,
        ITipoVehiculoHandler tipoVehiculoHandler)
    {
        var result = await tipoVehiculoHandler.UpdateAsync(tipoVehiculoRequest, id);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>, ProblemHttpResult>> Delete(
        [FromRoute] int id,
        [FromRoute] int softDelete,
        ITipoVehiculoHandler tipoVehiculoHandler)
    {
        if (softDelete < 0 || softDelete > 1)
        {
            return TypedResults.BadRequest("El valor softDelete debe ser 0 o 1");
        }

        var result = await tipoVehiculoHandler.Delete(id, softDelete == 1);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }
}