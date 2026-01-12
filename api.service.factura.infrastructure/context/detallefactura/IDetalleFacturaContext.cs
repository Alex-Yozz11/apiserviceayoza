using api.service.factura.domain.entities;

namespace api.service.factura.infrastructure.context.detallefactura;

public interface IDetalleFacturaContext
{
    Task<List<DetalleFactura>> GetAllAsync();
    Task<DetalleFactura> GetByIdAsync(int id);
    Task<DetalleFactura> InsertAsync(DetalleFactura detalleFactura);
    Task<(bool, string?)> UpdateAsync(DetalleFactura detalleFactura);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}