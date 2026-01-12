using api.service.factura.application.commons.dtos;
using api.service.factura.application.commons.mappings;
using api.service.factura.application.ifeatures;
using api.service.factura.infrastructure.context.vehiculo;

namespace api.service.factura.application.features;

public class VehiculoHandler : IVehiculoHandler
{
    private readonly Mappings _mapper;
    private readonly IVehiculoContext _context;

    public VehiculoHandler(IVehiculoContext context)
    {
        _mapper = new Mappings();
        _context = context;
    }

    public async Task<List<VehiculoResponseDto>> GetAll()
    {
        var vehiculos = await _context.GetAllAsync();
        return _mapper.ToResponseDto(vehiculos);
    }

    public async Task<VehiculoResponseDto> GetById(int id)
    {
        var vehiculo = await _context.GetByIdAsync(id);
        return _mapper.ToResponseDto(vehiculo);
    }

    public async Task<VehiculoResponseDto> Insert(VehiculoRequestDto vehiculoRequest)
    {
        var vehiculo = _mapper.ToEntity(vehiculoRequest);
        var vehiculoResponse = await _context.InsertAsync(vehiculo);
        return _mapper.ToResponseDto(vehiculoResponse);
    }

    public async Task<(bool, string?)> UpdateAsync(VehiculoRequestDto vehiculoRequest, int id)
    {
        var vehiculo = _mapper.ToEntity(vehiculoRequest);
        vehiculo.VehiculoId = id;

        var result = await _context.UpdateAsync(vehiculo);
        return result;
    }

    public async Task<(bool, string?)> Delete(int id, bool softDelete)
    {
        var result = await _context.Delete(id, softDelete);
        return result;
    }
}