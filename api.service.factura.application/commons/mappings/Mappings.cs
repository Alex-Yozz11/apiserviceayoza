using api.service.factura.application.commons.dtos;
using api.service.factura.domain.clases;
using Riok.Mapperly.Abstractions;

namespace api.service.factura.application.commons.mappings;

[Mapper]
public partial class Mappings
{
    // Cliente
    public partial ClienteResponseDto ToResponseDto(Cliente cliente);
    public partial List<ClienteResponseDto> ToResponseDto(List<Cliente> clientes);
    public partial Cliente ToRequestDto(ClienteRequestDto clienteRequestDto);

    // Vehiculo
    public partial VehiculoResponseDto ToResponseDto(Vehiculo vehiculo);
    public partial List<VehiculoResponseDto> ToResponseDto(List<Vehiculo> vehiculos);
    public partial Vehiculo ToRequestDto(VehiculoRequestDto vehiculoRequestDto);

    // TipoVehiculo
    public partial TipoVehiculoResponseDto ToResponseDto(TipoVehiculo tipoVehiculo);
    public partial List<TipoVehiculoResponseDto> ToResponseDto(List<TipoVehiculo> tiposVehiculo);
    public partial TipoVehiculo ToRequestDto(TipoVehiculoRequestDto tipoVehiculoRequestDto);

    // Vendedor
    public partial VendedorResponseDto ToResponseDto(Vendedor vendedor);
    public partial List<VendedorResponseDto> ToResponseDto(List<Vendedor> vendedores);
    public partial Vendedor ToRequestDto(VendedorRequestDto vendedorRequestDto);

    // Factura
    public partial FacturaResponseDto ToResponseDto(Factura factura);
    public partial List<FacturaResponseDto> ToResponseDto(List<Factura> facturas);
    public partial Factura ToRequestDto(FacturaRequestDto facturaRequestDto);

    // DetalleFactura
    public partial DetalleFacturaResponseDto ToResponseDto(DetalleFactura detalleFactura);
    public partial List<DetalleFacturaResponseDto> ToResponseDto(List<DetalleFactura> detallesFactura);
    public partial DetalleFactura ToRequestDto(DetalleFacturaRequestDto detalleFacturaRequestDto);
}