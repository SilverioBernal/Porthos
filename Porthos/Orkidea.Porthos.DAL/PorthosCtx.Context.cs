﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Orkidea.Porthos.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Orkidea.Porthos.Entities;
    
    public partial class PropiedadHorizontalEntities : DbContext
    {
        public PropiedadHorizontalEntities()
            : base("name=PropiedadHorizontalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Balance> Balance { get; set; }
        public DbSet<BalanceDetail> BalanceDetail { get; set; }
        public DbSet<BalancePaymentSupport> BalancePaymentSupport { get; set; }
        public DbSet<Concept> Concept { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectDistribution> ProjectDistribution { get; set; }
    }
}
