namespace api.service.factura.application.commons.dtos;

public sealed record VehiculoResponseDto(
    int VehiculoId,
    string Marca,
    string Modelo,
    int Anio,
    decimal Precio,
    string TipoVehiculoNombre,
    bool Activo
);