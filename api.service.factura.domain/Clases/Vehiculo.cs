using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.service.factura.infrastructure;

[Table("vehiculo", Schema = "ayoza")]
public partial class Vehiculo
{
    [Key]
    [Column("id_vehiculo")]
    public int IdVehiculo { get; set; }

    [Column("marca")]
    [StringLength(100)]
    public string Marca { get; set; } = null!;

    [Column("modelo")]
    [StringLength(100)]
    public string Modelo { get; set; } = null!;

    [Column("anio")]
    public int? Anio { get; set; }

    [Column("precio")]
    [Precision(10, 2)]
    public decimal Precio { get; set; }

    [Column("id_tipo_vehiculo")]
    public int IdTipoVehiculo { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }

    [Column("creado_en", TypeName = "timestamp without time zone")]
    public DateTime? CreadoEn { get; set; }

    [Column("actualizado_en", TypeName = "timestamp without time zone")]
    public DateTime? ActualizadoEn { get; set; }

    [InverseProperty("IdVehiculoNavigation")]
    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [ForeignKey("IdTipoVehiculo")]
    [InverseProperty("Vehiculos")]
    public virtual TipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;
}
