using System;
using System.Collections.Generic;

namespace api.service.factura.infrastructure.ClasesTemp;

public partial class DetalleFactura
{
    public int IdDetalle { get; set; }

    public int IdFactura { get; set; }

    public int IdVehiculo { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal SubtotalLinea { get; set; }

    public bool? Activo { get; set; }

    public DateTime? CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
