using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.service.factura.infrastructure;

[Table("factura", Schema = "ayoza")]
public partial class Factura
{
    [Key]
    [Column("id_factura")]
    public int IdFactura { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("id_vendedor")]
    public int IdVendedor { get; set; }

    [Column("fecha", TypeName = "timestamp without time zone")]
    public DateTime? Fecha { get; set; }

    [Column("estado")]
    [StringLength(20)]
    public string? Estado { get; set; }

    [Column("subtotal")]
    [Precision(10, 2)]
    public decimal Subtotal { get; set; }

    [Column("impuesto")]
    [Precision(10, 2)]
    public decimal? Impuesto { get; set; }

    [Column("total")]
    [Precision(10, 2)]
    public decimal Total { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }

    [Column("creado_en", TypeName = "timestamp without time zone")]
    public DateTime? CreadoEn { get; set; }

    [Column("actualizado_en", TypeName = "timestamp without time zone")]
    public DateTime? ActualizadoEn { get; set; }

    [InverseProperty("IdFacturaNavigation")]
    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Facturas")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdVendedor")]
    [InverseProperty("Facturas")]
    public virtual Vendedor IdVendedorNavigation { get; set; } = null!;
}
