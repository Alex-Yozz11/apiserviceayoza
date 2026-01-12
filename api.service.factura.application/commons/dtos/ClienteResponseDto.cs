namespace api.service.factura.application.commons.dtos;

public sealed record ClienteResponseDto(
    int ClienteId,
    string TipoIdentificacion,
    string Identificacion,
    string NombreCompleto,
    string? Telefono,
    string Email,
    bool Activo
);