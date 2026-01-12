namespace api.service.factura.application.commons.dtos;

public sealed record DetalleFacturaRequestDto(
    int IdVehiculo,
    int Cantidad,
    decimal PrecioUnitario
);