namespace api.service.factura.application.commons.dtos;

public sealed record VendedorRequestDto(
    string Nombre,
    string? Telefono
);