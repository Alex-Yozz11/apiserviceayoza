namespace api.service.factura.application.commons.dtos;

public sealed record VendedorResponseDto(
    int IdVendedor,
    string Nombre,
    string? Telefono,
    bool Activo
);