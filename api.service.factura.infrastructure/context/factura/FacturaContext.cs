using api.service.factura.domain.entities;
using api.service.factura.infrastructure.context;

namespace api.service.factura.infrastructure.context.factura;

public class FacturaContext : IFacturaContext
{
    private readonly IContextGeneral<Factura> _context;

    public FacturaContext(IContextGeneral<Factura> context)
    {
        _context = context;
    }

    public async Task<List<Factura>> GetAllAsync()
    {
        return await _context.GetAll();
    }

    public async Task<Factura> GetByIdAsync(int id)
    {
        Factura factura = await _context.GetById(id) ?? new Factura();
        return factura;
    }

    public async Task<Factura> InsertAsync(Factura factura)
    {
        return await _context.Add(factura);
    }

    public async Task<(bool, string?)> UpdateAsync(Factura factura)
    {
        bool isUpdate = false;
        var result = await _context.GetById(factura.FacturaId);

        if (result != null)
        {
            if (factura.Total != result.Total)
            {
                result.Total = factura.Total;
                isUpdate = true;
            }

            if (factura.ClienteId != result.ClienteId)
            {
                result.ClienteId = factura.ClienteId;
                isUpdate = true;
            }

            if (factura.VendedorId != result.VendedorId)
            {
                result.VendedorId = factura.VendedorId;
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