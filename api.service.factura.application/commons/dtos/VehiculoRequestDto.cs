namespace api.service.factura.application.commons.dtos;

public sealed record VehiculoRequestDto(
    string Marca,
    string Modelo,
    int Anio,
    decimal Precio,
    int IdTipoVehiculo
);