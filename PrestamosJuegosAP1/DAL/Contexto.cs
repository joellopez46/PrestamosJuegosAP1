using Microsoft.EntityFrameworkCore;
using PrestamosJuegosAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PrestamosJuegosAP1.DAL
{
    class Contexto : DbContext
    {
        public DbSet<Amigos>Amigos { get; set; }
        public DbSet<Juegos>Juegos { get; set; }
        public DbSet<Prestamos>Prestamos { get; set; }
        public DbSet<Entradas>Entradas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA\PrestamosJuegosAp1.db");
        }
    }
}