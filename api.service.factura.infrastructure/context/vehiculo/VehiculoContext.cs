using api.service.factura.domain.entities;
using api.service.factura.infrastructure.context;

namespace api.service.factura.infrastructure.context.vehiculo;

public class VehiculoContext : IVehiculoContext
{
    private readonly IContextGeneral<Vehiculo> _context;

    public VehiculoContext(IContextGeneral<Vehiculo> context)
    {
        _context = context;
    }

    public async Task<List<Vehiculo>> GetAllAsync()
    {
        return await _context.GetAll();
    }

    public async Task<Vehiculo> GetByIdAsync(int id)
    {
        Vehiculo vehiculo = await _context.GetById(id) ?? new Vehiculo();
        return vehiculo;
    }

    public async Task<Vehiculo> InsertAsync(Vehiculo vehiculo)
    {
        return await _context.Add(vehiculo);
    }

    public async Task<(bool, string?)> UpdateAsync(Vehiculo vehiculo)
    {
        bool isUpdate = false;
        var result = await _context.GetById(vehiculo.VehiculoId);

        if (result != null)
        {
            if (!string.IsNullOrEmpty(vehiculo.Placa) && vehiculo.Placa != result.Placa)
            {
                result.Placa = vehiculo.Placa;
                isUpdate = true;
            }

            if (!string.IsNullOrEmpty(vehiculo.Color) && vehiculo.Color != result.Color)
            {
                result.Color = vehiculo.Color;
                isUpdate = true;
            }

            if (vehiculo.TipoVehiculoId != result.TipoVehiculoId)
            {
                result.TipoVehiculoId = vehiculo.TipoVehiculoId;
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