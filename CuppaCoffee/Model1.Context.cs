﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CuppaCoffee
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CuppaDBEntities : DbContext
    {
        public CuppaDBEntities()
            : base("name=CuppaDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<drink_sizes> drink_sizes { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product_types> product_types { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<drink> drinks { get; set; }
        public virtual DbSet<food> foods { get; set; }
        public virtual DbSet<order_line> order_line { get; set; }
    }
}
