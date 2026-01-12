using api.service.factura.application.commons.dtos;
using api.service.factura.application.ifeatures;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.service.factura.presentation.endpoints;

public static class ClienteEndpoints
{
    public static RouteGroupBuilder MapCliente(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAll);
        builder.MapGet("/{id:int}", GetById);
        builder.MapPost("/", Insert);
        builder.MapPatch("/{id:int}", Update);
        builder.MapDelete("/{id:int}/{softDelete:int}", Delete);
        return builder;
    }

    static async Task<Results<Ok<List<ClienteResponseDto>>, ProblemHttpResult>> GetAll(IClienteHandler clienteHandler)
    {
        return TypedResults.Ok(await clienteHandler.GetAll());
    }

    static async Task<Results<Ok<ClienteResponseDto>, NotFound<string>, ProblemHttpResult>> GetById(
        [FromRoute] int id,
        IClienteHandler clienteHandler)
    {
        var cliente = await clienteHandler.GetById(id);
        if (cliente.ClienteId == 0)
        {
            return TypedResults.NotFound("No encontrado");
        }
        return TypedResults.Ok(cliente);
    }

    static async Task<Results<Created<ClienteResponseDto>, ProblemHttpResult>> Insert(
        [FromBody] ClienteRequestDto clienteRequest,
        IClienteHandler clienteHandler)
    {
        var cliente = await clienteHandler.Insert(clienteRequest);
        return TypedResults.Created($"/v1/cliente/{cliente.ClienteId}", cliente);
    }

    static async Task<Results<NoContent, NotFound<string>, ProblemHttpResult>> Update(
        [FromRoute] int id,
        [FromBody] ClienteRequestDto clienteRequest,
        IClienteHandler clienteHandler)
    {
        var result = await clienteHandler.UpdateAsync(clienteRequest, id);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>, ProblemHttpResult>> Delete(
        [FromRoute] int id,
        [FromRoute] int softDelete,
        IClienteHandler clienteHandler)
    {
        if (softDelete < 0 || softDelete > 1)
        {
            return TypedResults.BadRequest("El valor softDelete debe ser 0 o 1");
        }

        var result = await clienteHandler.Delete(id, softDelete == 1);

        if (!result.Item1 && result.Item2 != null)
        {
            return TypedResults.NotFound(result.Item2);
        }

        return TypedResults.NoContent();
    }
}