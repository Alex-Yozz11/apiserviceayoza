namespace api.service.factura.application.commons.dtos;

public sealed record DetalleFacturaResponseDto(
    int IdDetalle,
    string VehiculoModelo,
    int Cantidad,
    decimal PrecioUnitario,
    decimal SubtotalLinea
);