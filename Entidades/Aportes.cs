using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPersonas.Entidades
{
    public class Aportes
    {
        [Key]
        public int AporteId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int PersonaId { get; set; }
        public string Concepto { get; set; }
        public float Monto { get; set; }

        [ForeignKey("PersonaId")]
        public virtual Personas Persona { get; set; }

        [ForeignKey("AporteId")]
        public List<AportesDetalle> AporteDetalle { get; set; } = new List<AportesDetalle>();
    }
}
