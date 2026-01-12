using api.service.factura.application.commons.dtos;

namespace api.service.factura.application.ifeatures;

public interface IVehiculoHandler
{
    Task<List<VehiculoResponseDto>> GetAll();
    Task<VehiculoResponseDto> GetById(int id);
    Task<VehiculoResponseDto> Insert(VehiculoRequestDto vehiculoRequest);
    Task<(bool, string?)> UpdateAsync(VehiculoRequestDto vehiculoRequest, int id);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}