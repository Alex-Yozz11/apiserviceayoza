using api.service.factura.domain.entities;
using api.service.factura.infrastructure.context;

namespace api.service.factura.infrastructure.context.cliente;

public class ClienteContext : IClienteContext
{
    private readonly IContextGeneral<Cliente> _context;

    public ClienteContext(IContextGeneral<Cliente> context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> GetAllAsync()
    {
        return await _context.GetAll();
    }

    public async Task<Cliente> GetByIdAsync(int id)
    {
        Cliente cliente = await _context.GetById(id) ?? new Cliente();
        return cliente;
    }

    public async Task<Cliente> InsertAsync(Cliente cliente)
    {
        return await _context.Add(cliente);
    }

    public async Task<(bool, string?)> UpdateAsync(Cliente cliente)
    {
        bool isUpdate = false;
        var result = await _context.GetById(cliente.ClienteId);

        if (result != null)
        {
            if (!string.IsNullOrEmpty(cliente.Direccion) && cliente.Direccion != result.Direccion)
            {
                result.Direccion = cliente.Direccion;
                isUpdate = true;
            }

            if (!string.IsNullOrEmpty(cliente.Telefono) && cliente.Telefono != result.Telefono)
            {
                result.Telefono = cliente.Telefono;
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