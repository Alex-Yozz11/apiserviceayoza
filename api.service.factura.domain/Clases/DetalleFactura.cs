using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.service.factura.infrastructure;

[Table("detalle_factura", Schema = "ayoza")]
public partial class DetalleFactura
{
    [Key]
    [Column("id_detalle")]
    public int IdDetalle { get; set; }

    [Column("id_factura")]
    public int IdFactura { get; set; }

    [Column("id_vehiculo")]
    public int IdVehiculo { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_unitario")]
    [Precision(10, 2)]
    public decimal PrecioUnitario { get; set; }

    [Column("subtotal_linea")]
    [Precision(10, 2)]
    public decimal SubtotalLinea { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }

    [Column("creado_en", TypeName = "timestamp without time zone")]
    public DateTime? CreadoEn { get; set; }

    [Column("actualizado_en", TypeName = "timestamp without time zone")]
    public DateTime? ActualizadoEn { get; set; }

    [ForeignKey("IdFactura")]
    [InverseProperty("DetalleFacturas")]
    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    [ForeignKey("IdVehiculo")]
    [InverseProperty("DetalleFacturas")]
    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
