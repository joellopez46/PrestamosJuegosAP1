﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrestamosJuegosAP1.Entidades
{
    public class Juegos
    {
        [Key]
        public int JuegoId { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }
    }
}