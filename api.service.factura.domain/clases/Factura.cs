using System;
using System.Collections.Generic;

namespace api.service.factura.infrastructure.ClasesTemp;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int IdCliente { get; set; }

    public int IdVendedor { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estado { get; set; }

    public decimal Subtotal { get; set; }

    public decimal? Impuesto { get; set; }

    public decimal Total { get; set; }

    public bool? Activo { get; set; }

    public DateTime? CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Vendedor IdVendedorNavigation { get; set; } = null!;
}
