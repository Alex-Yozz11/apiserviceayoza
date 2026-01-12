namespace api.service.factura.application.commons.dtos;

public sealed record TipoVehiculoRequestDto(
    string Nombre,
    string? Descripcion
);