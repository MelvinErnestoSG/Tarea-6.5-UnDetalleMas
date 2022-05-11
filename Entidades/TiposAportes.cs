using System.ComponentModel.DataAnnotations;

namespace GestionPersonas.Entidades
{
    public class TiposAportes
    {
        [Key]
        public int TipoAporteId { get; set; }
        public string Descripcion { get; set; }
        public float Meta { get; set; }
        public float Logrado { get; set; }
    }
}
