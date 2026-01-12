using api.service.factura.application.commons.dtos;
using api.service.factura.application.commons.mappings;
using api.service.factura.application.ifeatures;
using api.service.factura.infrastructure.context.factura;

namespace api.service.factura.application.features;

public class FacturaHandler : IFacturaHandler
{
    private readonly Mappings _mapper;
    private readonly IFacturaContext _context;

    public FacturaHandler(IFacturaContext context)
    {
        _mapper = new Mappings();
        _context = context;
    }

    public async Task<List<FacturaResponseDto>> GetAll()
    {
        var facturas = await _context.GetAllAsync();
        return _mapper.ToResponseDto(facturas);
    }

    public async Task<FacturaResponseDto> GetById(int id)
    {
        var factura = await _context.GetByIdAsync(id);
        return _mapper.ToResponseDto(factura);
    }

    public async Task<FacturaResponseDto> Insert(FacturaRequestDto facturaRequest)
    {
        var factura = _mapper.ToEntity(facturaRequest);
        var facturaResponse = await _context.InsertAsync(factura);
        return _mapper.ToResponseDto(facturaResponse);
    }

    public async Task<(bool, string?)> UpdateAsync(FacturaRequestDto facturaRequest, int id)
    {
        var factura = _mapper.ToEntity(facturaRequest);
        factura.FacturaId = id;

        var result = await _context.UpdateAsync(factura);
        return result;
    }

    public async Task<(bool, string?)> Delete(int id, bool softDelete)
    {
        var result = await _context.Delete(id, softDelete);
        return result;
    }
}