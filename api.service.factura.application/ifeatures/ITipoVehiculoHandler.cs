using api.service.factura.application.commons.dtos;

namespace api.service.factura.application.ifeatures;

public interface ITipoVehiculoHandler
{
    Task<List<TipoVehiculoResponseDto>> GetAll();
    Task<TipoVehiculoResponseDto> GetById(int id);
    Task<TipoVehiculoResponseDto> Insert(TipoVehiculoRequestDto tipoRequest);
    Task<(bool, string?)> UpdateAsync(TipoVehiculoRequestDto tipoRequest, int id);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}