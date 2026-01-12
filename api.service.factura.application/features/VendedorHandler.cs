using api.service.factura.application.commons.dtos;
using api.service.factura.application.commons.mappings;
using api.service.factura.application.ifeatures;
using api.service.factura.infrastructure.context.vendedor;

namespace api.service.factura.application.features;

public class VendedorHandler : IVendedorHandler
{
    private readonly Mappings _mapper;
    private readonly IVendedorContext _context;

    public VendedorHandler(IVendedorContext context)
    {
        _mapper = new Mappings();
        _context = context;
    }

    public async Task<List<VendedorResponseDto>> GetAll()
    {
        var vendedores = await _context.GetAllAsync();
        return _mapper.ToResponseDto(vendedores);
    }

    public async Task<VendedorResponseDto> GetById(int id)
    {
        var vendedor = await _context.GetByIdAsync(id);
        return _mapper.ToResponseDto(vendedor);
    }

    public async Task<VendedorResponseDto> Insert(VendedorRequestDto vendedorRequest)
    {
        var vendedor = _mapper.ToEntity(vendedorRequest);
        var vendedorResponse = await _context.InsertAsync(vendedor);
        return _mapper.ToResponseDto(vendedorResponse);
    }

    public async Task<(bool, string?)> UpdateAsync(VendedorRequestDto vendedorRequest, int id)
    {
        var vendedor = _mapper.ToEntity(vendedorRequest);
        vendedor.VendedorId = id;

        var result = await _context.UpdateAsync(vendedor);
        return result;
    }

    public async Task<(bool, string?)> Delete(int id, bool softDelete)
    {
        var result = await _context.Delete(id, softDelete);
        return result;
    }
}