using api.service.factura.domain.entities;

namespace api.service.factura.infrastructure.context.tipovehiculo;

public interface ITipoVehiculoContext
{
    Task<List<TipoVehiculo>> GetAllAsync();
    Task<TipoVehiculo> GetByIdAsync(int id);
    Task<TipoVehiculo> InsertAsync(TipoVehiculo tipoVehiculo);
    Task<(bool, string?)> UpdateAsync(TipoVehiculo tipoVehiculo);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}