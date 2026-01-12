namespace api.service.factura.application.commons.dtos;

public sealed record FacturaRequestDto(
    int IdCliente,
    int IdVendedor,
    decimal Subtotal,
    decimal Impuesto,
    decimal Total,
    List<DetalleFacturaRequestDto> Detalles
);