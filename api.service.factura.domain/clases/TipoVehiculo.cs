using System;
using System.Collections.Generic;

namespace api.service.factura.infrastructure.ClasesTemp;

public partial class TipoVehiculo
{
    public int IdTipoVehiculo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
