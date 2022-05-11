using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPersonas.Entidades
{
    public class AportesDetalle
    {
        [Key]
        public int Id { get; set; }
        public int TipoAporteId { get; set; }
        public int AporteId { get; set; }
        public float Valor { get; set; }

        [ForeignKey("TipoAporteId")]
        public TiposAportes TiposAporte { get; set; }
    }
}
