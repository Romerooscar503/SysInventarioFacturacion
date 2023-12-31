﻿using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.AccesoADatos
{
    public class BDContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        public DbSet <DetalleFactura> DetalleFactura { get; set; }
		public DbSet<Categoria> Categoria { get; set; }
		public DbSet<Pedido> Pedido { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Factura> Factura { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"workstation id=SysInventarioFacturacion503.mssql.somee.com;packet size=4096;user id=romerooscar_SQLLogin_1;pwd=awaosafn8m;data source=SysInventarioFacturacion503.mssql.somee.com;persist security info=False;initial catalog=SysInventarioFacturacion503;Encrypt=False;TrustServerCertificate=False;");
            optionsBuilder.UseSqlServer(@"workstation id=SysInventarioFacturacion38.mssql.somee.com;packet size=4096;user id=Corteznestor_SQLLogin_1;pwd=v1f8t8p9b9;data source=SysInventarioFacturacion38.mssql.somee.com;persist security info=False;initial catalog=SysInventarioFacturacion38;Encrypt=False;TrustServerCertificate=False;");

            //optionsBuilder.UseSqlServer(@"Data Source=OSCARROMERO\SQLEXPRESS;Initial Catalog=SysInventarioFacturacion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Factura>().HasMany(f => f.DetalleFactura).WithOne(df => df.Factura).HasForeignKey(df => df.IdFactura);
        //}
    }
}
