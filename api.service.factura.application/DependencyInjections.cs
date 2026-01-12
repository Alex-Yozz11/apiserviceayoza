using Microsoft.Extensions.DependencyInjection;
using api.service.factura.application.ifeatures;
using api.service.factura.application.features;

namespace api.service.factura.application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registro de Handlers
            services.AddScoped<IClienteHandler, ClienteHandler>();
            services.AddScoped<IVehiculoHandler, VehiculoHandler>();
            services.AddScoped<ITipoVehiculoHandler, TipoVehiculoHandler>();
            services.AddScoped<IFacturaHandler, FacturaHandler>();
            services.AddScoped<IDetalleFacturaHandler, DetalleFacturaHandler>();
            services.AddScoped<IVendedorHandler, VendedorHandler>();

            return services;
        }
    }
}