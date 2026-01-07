using System;
using System.Collections.Generic;

namespace api.service.factura.infrastructure.ClasesTemp;

public partial class Vendedor
{
    public int IdVendedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public bool? Activo { get; set; }

    public DateTime? CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
