using api.service.factura.domain.entities;

namespace api.service.factura.infrastructure.context.vendedor;

public interface IVendedorContext
{
    Task<List<Vendedor>> GetAllAsync();
    Task<Vendedor> GetByIdAsync(int id);
    Task<Vendedor> InsertAsync(Vendedor vendedor);
    Task<(bool, string?)> UpdateAsync(Vendedor vendedor);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}