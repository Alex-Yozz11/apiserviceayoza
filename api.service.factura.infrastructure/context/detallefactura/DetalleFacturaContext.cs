using api.service.factura.domain.entities;
using api.service.factura.infrastructure.context;

namespace api.service.factura.infrastructure.context.detallefactura;

public class DetalleFacturaContext : IDetalleFacturaContext
{
    private readonly IContextGeneral<DetalleFactura> _context;

    public DetalleFacturaContext(IContextGeneral<DetalleFactura> context)
    {
        _context = context;
    }

    public async Task<List<DetalleFactura>> GetAllAsync()
    {
        return await _context.GetAll();
    }

    public async Task<DetalleFactura> GetByIdAsync(int id)
    {
        DetalleFactura detalle = await _context.GetById(id) ?? new DetalleFactura();
        return detalle;
    }

    public async Task<DetalleFactura> InsertAsync(DetalleFactura detalleFactura)
    {
        return await _context.Add(detalleFactura);
    }

    public async Task<(bool, string?)> UpdateAsync(DetalleFactura detalleFactura)
    {
        bool isUpdate = false;
        var result = await _context.GetById(detalleFactura.DetalleFacturaId);

        if (result != null)
        {
            if (detalleFactura.Cantidad != result.Cantidad)
            {
                result.Cantidad = detalleFactura.Cantidad;
                isUpdate = true;
            }

            if (detalleFactura.PrecioUnitario != result.PrecioUnitario)
            {
                result.PrecioUnitario = detalleFactura.PrecioUnitario;
                isUpdate = true;
            }

            if (detalleFactura.FacturaId != result.FacturaId)
            {
                result.FacturaId = detalleFactura.FacturaId;
                isUpdate = true;
            }

            if (detalleFactura.VehiculoId != result.VehiculoId)
            {
                result.VehiculoId = detalleFactura.VehiculoId;
                isUpdate = true;
            }

            if (isUpdate)
            {
                result.FechaUpdate = DateTime.Now;
                await _context.Update(result);
                return (true, null);
            }
            else
            {
                return (false, null);
            }
        }

        return (false, "No encontrado");
    }

    public async Task<(bool, string?)> Delete(int id, bool softDelete)
    {
        var result = await _context.GetById(id);

        if (result != null)
        {
            result.Estado = softDelete; // soft delete
            result.FechaUpdate = DateTime.Now;
            await _context.Update(result);
            return (true, null);
        }

        return (false, "No encontrado");
    }
}