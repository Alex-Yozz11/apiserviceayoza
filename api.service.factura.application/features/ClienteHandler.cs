using api.service.factura.application.commons.dtos;
using api.service.factura.application.commons.mappings;
using api.service.factura.application.ifeatures;
using api.service.factura.infrastructure.context.cliente;

namespace api.service.factura.application.features;

public class ClienteHandler : IClienteHandler
{
    private readonly Mappings _mapper;
    private readonly IClienteContext _context;

    public ClienteHandler(IClienteContext context)
    {
        _mapper = new Mappings();
        _context = context;
    }

    public async Task<List<ClienteResponseDto>> GetAll()
    {
        var clientes = await _context.GetAllAsync();
        return _mapper.ToResponseDto(clientes);
    }

    public async Task<ClienteResponseDto> GetById(int id)
    {
        var cliente = await _context.GetByIdAsync(id);
        return _mapper.ToResponseDto(cliente);
    }

    public async Task<ClienteResponseDto> Insert(ClienteRequestDto clienteRequest)
    {
        var cliente = _mapper.ToEntity(clienteRequest);
        var clienteResponse = await _context.InsertAsync(cliente);
        return _mapper.ToResponseDto(clienteResponse);
    }

    public async Task<(bool, string?)> UpdateAsync(ClienteRequestDto clienteRequest, int id)
    {
        var cliente = _mapper.ToEntity(clienteRequest);
        cliente.ClienteId = id;

        var result = await _context.UpdateAsync(cliente);
        return result;
    }

    public async Task<(bool, string?)> Delete(int id, bool softDelete)
    {
        var result = await _context.Delete(id, softDelete);
        return result;
    }
}