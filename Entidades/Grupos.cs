using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPersonas.Entidades
{
    public class Grupos
    {
        [Key]
        public int GrupoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public int CantidadPersonas { get; set; }
        
        [ForeignKey("GrupoId")]
        public List<GruposDetalle> GrupoDetalle { get; set; } = new List<GruposDetalle>();
    }
}
