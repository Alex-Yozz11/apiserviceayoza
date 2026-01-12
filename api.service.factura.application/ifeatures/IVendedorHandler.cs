using api.service.factura.application.commons.dtos;

namespace api.service.factura.application.ifeatures;

public interface IVendedorHandler
{
    Task<List<VendedorResponseDto>> GetAll();
    Task<VendedorResponseDto> GetById(int id);
    Task<VendedorResponseDto> Insert(VendedorRequestDto vendedorRequest);
    Task<(bool, string?)> UpdateAsync(VendedorRequestDto vendedorRequest, int id);
    Task<(bool, string?)> Delete(int id, bool softDelete);
}