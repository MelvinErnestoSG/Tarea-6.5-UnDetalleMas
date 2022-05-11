using GestionPersonas.DAL;
using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestionPersonas.BLL
{
    public class AportesBLL
    {
        /// <summary>
        /// Permite encontrar una entidad en la base de datos por su id.
        /// </summary>
        /// <param name="id">La entidad que se desea buscar por su id.</param>
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Aportes.Any(e => e.AporteId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }

        /// <summary>
        /// Permite buscar una entidad en la base de datos.
        /// </summary>
        /// <param name="id">La entidad que se desea buscar.</param>
        public static Aportes Buscar(int id)
        {
            Aportes aporte = new Aportes();
            Contexto contexto = new Contexto();

            try
            {
                aporte = contexto.Aportes.Include(x => x.AporteDetalle)
                    .Where(x => x.AporteId == id)
                    .Include(x => x.AporteDetalle)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return aporte;
        }

        /// <summary>
        /// Permite guardar una entidad en la base de datos.
        /// </summary>
        /// <param name="aporte">La entidad que se desea insertar y modificar.</param>
        public static bool Guardar(Aportes aporte)
        {
            if (!Existe(aporte.AporteId))//si no existe insertamos
                return Insertar(aporte);
            else
                return Modificar(aporte);
        }

        /// <summary>
        /// Permite insertar una entidad en la base de datos.
        /// </summary>
        /// <param name="aporte">La entidad que se desea insertar.</param>
        private static bool Insertar(Aportes aporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Aportes.Add(aporte);

                foreach (var detalle in aporte.AporteDetalle)
                {
                    detalle.TiposAporte.Logrado += aporte.Monto;
                    //detalle.Persona.TotalAportado += detalle.Valor;
                }
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        /// <summary>
        /// Permite modificar una entidad en la base de datos.
        /// </summary>
        /// <param name="aporte">La entidad que se desea modificar.</param>
        private static bool Modificar(Aportes aporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var aporteAnterior = contexto.Aportes
                    .Where(x => x.AporteId == aporte.AporteId)
                    .Include(x => x.AporteDetalle)
                    .AsNoTracking()
                    .SingleOrDefault();

                foreach (var detalle in aporteAnterior.AporteDetalle)
                {
                    detalle.TiposAporte.Logrado -= detalle.Valor;
                }

                contexto.Database.ExecuteSqlRaw($"Delete From AportesDetalle Where Id={aporte.AporteId}");

                foreach (var item in aporte.AporteDetalle)
                {
                    item.TiposAporte.Logrado += item.Valor;
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(aporte).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        /// <summary>
        /// Permite Eliminar una entidad en la base de datos.
        /// </summary>
        /// <param name="id">La entidad que se desea eliminar.</param>
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var aporte = AportesBLL.Buscar(id);

                if (aporte != null)
                {
                    foreach (var detalle in aporte.AporteDetalle)
                    {
                        detalle.TiposAporte.Logrado -= detalle.Valor;
                    }

                    contexto.Aportes.Remove(aporte);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        /// <summary>
        /// Permite Listar una entidad en la base de datos.
        /// </summary>
        /// <param name="criterio">La entidad que se desea listar.</param>
        public static List<Aportes> GetList(Expression<Func<Aportes, bool>> criterio)
        {
            List<Aportes> Lista = new List<Aportes>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Aportes.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
    }
}
