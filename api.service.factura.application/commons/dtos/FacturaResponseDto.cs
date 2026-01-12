namespace api.service.factura.application.commons.dtos;

public sealed record FacturaResponseDto(
    int IdFactura,
    string ClienteNombre,
    string VendedorNombre,
    DateTime Fecha,
    string Estado,
    decimal Subtotal,
    decimal Impuesto,
    decimal Total,
    List<DetalleFacturaResponseDto> Detalles
);