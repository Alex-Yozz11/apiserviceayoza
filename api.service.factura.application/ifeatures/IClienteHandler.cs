using api.service.factura.application.commons.dtos;

namespace api.service.factura.application.ifeatures;

public interface IClienteHandler
{
    Task<List<ClienteResponseDto>> GetAll();
    Task<ClienteResponseDto> GetById(int id);
    Task<ClienteResponseDto> Insert(ClienteRequestDto clienteRequest);
    Task<(bool, string?)> UpdateAsync(ClienteRequestDto clienteRequest, int id);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}