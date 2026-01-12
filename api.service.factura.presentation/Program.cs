using api.service.factura.infrastructure;
using api.service.factura.application;
using api.service.factura.presentation.endpoints;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
var url = $"http://0.0.0.0:{port}";

#region servicios
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplicationServices();
#endregion servicios

var app = builder.Build();

#region middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // expone el JSON de OpenAPI
    app.UseSwagger(); // genera swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Factura API v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz (http://localhost:3000)
    });
}

app.UseHttpsRedirection();

// Endpoints agrupados
app.MapGroup("/v1/cliente").MapCliente();
app.MapGroup("/v1/vehiculo").MapVehiculo();
app.MapGroup("/v1/tipovehiculo").MapTipoVehiculo();
app.MapGroup("/v1/factura").MapFactura();
app.MapGroup("/v1/detallefactura").MapDetalleFactura();
app.MapGroup("/v1/vendedor").MapVendedor();
#endregion middleware

app.Run(url);