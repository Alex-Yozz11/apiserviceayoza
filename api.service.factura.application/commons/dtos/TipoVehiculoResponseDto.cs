namespace api.service.factura.application.commons.dtos;

public sealed record TipoVehiculoResponseDto(
    int IdTipoVehiculo,
    string Nombre,
    string? Descripcion,
    bool Activo
);