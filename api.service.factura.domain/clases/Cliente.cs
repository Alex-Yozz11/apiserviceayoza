using System;
using System.Collections.Generic;

namespace api.service.factura.infrastructure.ClasesTemp;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string TipoIdentificacion { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Email { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime? CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
