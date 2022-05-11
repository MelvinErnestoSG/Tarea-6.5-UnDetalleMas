using System.ComponentModel.DataAnnotations;

namespace GestionPersonas.Entidades
{
    public class Roles
    {
        [Key]
        public int RolId { get; set; }
        public string Descripcion { get; set; }
    }
}