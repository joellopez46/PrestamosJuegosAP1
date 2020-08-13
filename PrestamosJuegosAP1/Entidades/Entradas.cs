﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrestamosJuegosAP1.Entidades
{
    public class Entradas
    {
        [Key]
        public int EntradaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int JuegoId { get; set; }
        public int Cantidad { get; set; }
    }
}