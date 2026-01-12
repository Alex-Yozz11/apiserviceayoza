using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using api.service.factura.infrastructure.ClasesTemp;

namespace api.service.factura.infrastructure;

public partial class FacturaDbContext : DbContext
{
    public FacturaDbContext(DbContextOptions<FacturaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    public virtual DbSet<Vendedor> Vendedors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("cliente_pkey");

            entity.ToTable("cliente", "ayoza");

            entity.HasIndex(e => e.Email, "cliente_email_key").IsUnique();

            entity.HasIndex(e => new { e.TipoIdentificacion, e.Identificacion }, "cliente_tipo_identificacion_identificacion_key").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(13)
                .HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(10)
                .HasColumnName("tipo_identificacion");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("detalle_factura_pkey");

            entity.ToTable("detalle_factura", "ayoza");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.IdVehiculo).HasColumnName("id_vehiculo");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.SubtotalLinea)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal_linea");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_factura_id_factura_fkey");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_factura_id_vehiculo_fkey");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("factura_pkey");

            entity.ToTable("factura", "ayoza");

            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");
            entity.Property(e => e.Impuesto)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("impuesto");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura_id_cliente_fkey");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura_id_vendedor_fkey");
        });

        modelBuilder.Entity<TipoVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdTipoVehiculo).HasName("tipo_vehiculo_pkey");

            entity.ToTable("tipo_vehiculo", "ayoza");

            entity.HasIndex(e => e.Nombre, "tipo_vehiculo_nombre_key").IsUnique();

            entity.Property(e => e.IdTipoVehiculo).HasColumnName("id_tipo_vehiculo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo).HasName("vehiculo_pkey");

            entity.ToTable("vehiculo", "ayoza");

            entity.Property(e => e.IdVehiculo).HasColumnName("id_vehiculo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.IdTipoVehiculo).HasColumnName("id_tipo_vehiculo");
            entity.Property(e => e.Marca)
                .HasMaxLength(100)
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("modelo");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdTipoVehiculoNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.IdTipoVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehiculo_id_tipo_vehiculo_fkey");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.IdVendedor).HasName("vendedor_pkey");

            entity.ToTable("vendedor", "ayoza");

            entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
