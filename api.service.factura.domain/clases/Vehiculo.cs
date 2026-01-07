using System;
using System.Collections.Generic;

namespace api.service.factura.infrastructure.ClasesTemp;

public partial class Vehiculo
{
    public int IdVehiculo { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int? Anio { get; set; }

    public decimal Precio { get; set; }

    public int IdTipoVehiculo { get; set; }

    public bool? Activo { get; set; }

    public DateTime? CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual TipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;
}
