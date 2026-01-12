using api.service.factura.application.commons.dtos;
using api.service.factura.application.commons.mappings;
using api.service.factura.application.ifeatures;
using api.service.factura.infrastructure.context.tipovehiculo;

namespace api.service.factura.application.features;

public class TipoVehiculoHandler : ITipoVehiculoHandler
{
    private readonly Mappings _mapper;
    private readonly ITipoVehiculoContext _context;

    public TipoVehiculoHandler(ITipoVehiculoContext context)
    {
        _mapper = new Mappings();
        _context = context;
    }

    public async Task<List<TipoVehiculoResponseDto>> GetAll()
    {
        var tipos = await _context.GetAllAsync();
        return _mapper.ToResponseDto(tipos);
    }

    public async Task<TipoVehiculoResponseDto> GetById(int id)
    {
        var tipo = await _context.GetByIdAsync(id);
        return _mapper.ToResponseDto(tipo);
    }

    public async Task<TipoVehiculoResponseDto> Insert(TipoVehiculoRequestDto tipoRequest)
    {
        var tipo = _mapper.ToEntity(tipoRequest);
        var tipoResponse = await _context.InsertAsync(tipo);
        return _mapper.ToResponseDto(tipoResponse);
    }

    public async Task<(bool, string?)> UpdateAsync(TipoVehiculoRequestDto tipoRequest, int id)
    {
        var tipo = _mapper.ToEntity(tipoRequest);
        tipo.TipoVehiculoId = id;

        var result = await _context.UpdateAsync(tipo);
        return result;
    }

    public async Task<(bool, string?)> Delete(int id, bool softDelete)
    {
        var result = await _context.Delete(id, softDelete);
        return result;
    }
}