﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPAI2023
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PPAI_DSIEntities : DbContext
    {
        public PPAI_DSIEntities()
            : base("name=PPAI_DSIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Acciones> Acciones { get; set; }
        public DbSet<CambiosEstado> CambiosEstado { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Llamadas> Llamadas { get; set; }
        public DbSet<OpcionesLlamada> OpcionesLlamada { get; set; }
        public DbSet<OpcionesValidaciones> OpcionesValidaciones { get; set; }
        public DbSet<SubOpcionesLlamada> SubOpcionesLlamada { get; set; }
        public DbSet<Validaciones> Validaciones { get; set; }
    }
}
