using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPersonas.Entidades
{
    public class GruposDetalle
    {
        [Key]
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public int PersonaId { get; set; }
        public string Orden { get; set; }
       
        [ForeignKey("PersonaId")]
        public Personas Persona { get; set; }
    }
}
