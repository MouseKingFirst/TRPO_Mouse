﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TRPO_Mouse.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<library_authors> library_authors { get; set; }
        public virtual DbSet<library_book_example> library_book_example { get; set; }
        public virtual DbSet<library_books> library_books { get; set; }
        public virtual DbSet<library_sessions> library_sessions { get; set; }
        public virtual DbSet<library_users> library_users { get; set; }
        public virtual DbSet<library_users_data> library_users_data { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<library_books_relation> library_books_relation { get; set; }
    }
}
