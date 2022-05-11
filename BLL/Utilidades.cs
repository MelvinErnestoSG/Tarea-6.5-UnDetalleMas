
namespace GestionPersonas.BLL
{
    public class Utilidades
    {
        /// <summary>
        /// Para evitar errores con int.TryParse(valor, out retorno) 
        /// En la ejecución de los botones de mantenimiento(buscar, nuevo, guardar, eliminar).
        /// </summary>
        /// <param name="valor"></param>
        public static int ToInt(string valor)
        {
            int retorno = 0;
            int.TryParse(valor, out retorno);
            return retorno;
        }
    }
}
