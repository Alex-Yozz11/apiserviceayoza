using api.service.factura.application.commons.dtos;

namespace api.service.factura.application.ifeatures;

public interface IDetalleFacturaHandler
{
    Task<List<DetalleFacturaResponseDto>> GetAll();
    Task<DetalleFacturaResponseDto> GetById(int id);
    Task<DetalleFacturaResponseDto> Insert(DetalleFacturaRequestDto detalleRequest);
    Task<(bool, string?)> UpdateAsync(DetalleFacturaRequestDto detalleRequest, int id);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}