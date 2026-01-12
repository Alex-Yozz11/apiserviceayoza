using api.service.factura.domain.entities;

namespace api.service.factura.infrastructure.context.vehiculo;

public interface IVehiculoContext
{
    Task<List<Vehiculo>> GetAllAsync();
    Task<Vehiculo> GetByIdAsync(int id);
    Task<Vehiculo> InsertAsync(Vehiculo vehiculo);
    Task<(bool, string?)> UpdateAsync(Vehiculo vehiculo);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}