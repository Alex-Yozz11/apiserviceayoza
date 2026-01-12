using api.service.factura.application.commons.dtos;

namespace api.service.factura.application.ifeatures;

public interface IFacturaHandler
{
    Task<List<FacturaResponseDto>> GetAll();
    Task<FacturaResponseDto> GetById(int id);
    Task<FacturaResponseDto> Insert(FacturaRequestDto facturaRequest);
    Task<(bool, string?)> UpdateAsync(FacturaRequestDto facturaRequest, int id);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}