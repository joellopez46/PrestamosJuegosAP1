﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrestamosJuegosAP1.Entidades
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int AmigoId { get; set; }
        public string Observacion { get; set; }
        public int CantidadJuegos { get; set; }

        [ForeignKey("PrestamoId")]
        public virtual List<PrestamosDetalle> Detalles { get; set; } = new List<PrestamosDetalle>();
    }
}
