using api.service.factura.application.commons.dtos;
using api.service.factura.application.commons.mappings;
using api.service.factura.application.ifeatures;
using api.service.factura.infrastructure.context.detallefactura;

namespace api.service.factura.application.features;

public class DetalleFacturaHandler : IDetalleFacturaHandler
{
    private readonly Mappings _mapper;
    private readonly IDetalleFacturaContext _context;

    public DetalleFacturaHandler(IDetalleFacturaContext context)
    {
        _mapper = new Mappings();
        _context = context;
    }

    public async Task<List<DetalleFacturaResponseDto>> GetAll()
    {
        var detalles = await _context.GetAllAsync();
        return _mapper.ToResponseDto(detalles);
    }

    public async Task<DetalleFacturaResponseDto> GetById(int id)
    {
        var detalle = await _context.GetByIdAsync(id);
        return _mapper.ToResponseDto(detalle);
    }

    public async Task<DetalleFacturaResponseDto> Insert(DetalleFacturaRequestDto detalleRequest)
    {
        var detalle = _mapper.ToEntity(detalleRequest);
        var detalleResponse = await _context.InsertAsync(detalle);
        return _mapper.ToResponseDto(detalleResponse);
    }

    public async Task<(bool, string?)> UpdateAsync(DetalleFacturaRequestDto detalleRequest, int id)
    {
        var detalle = _mapper.ToEntity(detalleRequest);
        detalle.DetalleFacturaId = id;

        var result = await _context.UpdateAsync(detalle);
        return result;
    }

    public async Task<(bool, string?)> Delete(int id, bool softDelete)
    {
        var result = await _context.Delete(id, softDelete);
        return result;
    }
}