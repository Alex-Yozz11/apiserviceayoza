using api.service.factura.infrastructure.context;
using api.service.factura.infrastructure.context.cliente;
using api.service.factura.infrastructure.context.vehiculo;
using api.service.factura.infrastructure.context.tipovehiculo;
using api.service.factura.infrastructure.context.factura;
using api.service.factura.infrastructure.context.detallefactura;
using api.service.factura.infrastructure.context.vendedor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api.service.factura.infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
                                                    IConfiguration configuration)
    {
        // Configuración del DbContext con PostgreSQL
        services.AddDbContext<FacturaDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                              builder => builder.MigrationsAssembly(typeof(FacturaDbContext).Assembly.FullName)
                                                .EnableRetryOnFailure(
                                                    maxRetryCount: 5,
                                                    maxRetryDelay: TimeSpan.FromSeconds(10),
                                                    errorCodesToAdd: null
                                                ))
        );

        // Registro del ContextGeneral genérico
        services.AddScoped(typeof(IContextGeneral<>), typeof(ContextGeneral<>));

        // Registro de Contexts específicos
        services.AddScoped<IClienteContext, ClienteContext>();
        services.AddScoped<IVehiculoContext, VehiculoContext>();
        services.AddScoped<ITipoVehiculoContext, TipoVehiculoContext>();
        services.AddScoped<IFacturaContext, FacturaContext>();
        services.AddScoped<IDetalleFacturaContext, DetalleFacturaContext>();
        services.AddScoped<IVendedorContext, VendedorContext>();

        return services;
    }
}