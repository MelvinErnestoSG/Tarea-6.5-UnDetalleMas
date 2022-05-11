using GestionPersonas.DAL;
using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestionPersonas.BLL
{
    public class TiposAportesBLL
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
                encontrado = contexto.TiposAportes.Any(t => t.TipoAporteId == id);
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
        /// Permite encontrar una entidad en la base de datos por su descripción.
        /// </summary>
        /// <param name="descripcion">La entidad que se desea buscar por su descripción.</param>
        public static bool ExisteDescripcion(string descripcion)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.TiposAportes.Any(r => r.Descripcion == descripcion);
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
        public static TiposAportes Buscar(int id)
        {
            Contexto contexto = new Contexto();
            TiposAportes tiposAporte;

            try
            {
                tiposAporte = contexto.TiposAportes.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return tiposAporte;
        }

        /// <summary>
        /// Permite Guardar una entidad en la base de datos.
        /// </summary>
        /// <param name="tipoAporte">La entidad que se desea insertar y modificar.</param>
        public static bool Guardar(TiposAportes tipoAporte)
        {
            if (!Existe(tipoAporte.TipoAporteId))
            {
                return Insertar(tipoAporte);
            }
            else
            {
                return Modificar(tipoAporte);
            }
        }

        /// <summary>
        /// Permite insertar una entidad en la base de datos.
        /// </summary>
        /// <param name="tipoAporte">La entidad que se desea insertar.</param>
        private static bool Insertar(TiposAportes tipoAporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.TiposAportes.Add(tipoAporte);
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
        /// Permite modificar una entidad en la base de datos.
        /// </summary>
        /// <param name="tipoAporte">La entidad que se desea modificar.</param>
        public static bool Modificar(TiposAportes tipoAporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(tipoAporte).State = EntityState.Modified;
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
        /// Permite eliminar una entidad en la base de datos.
        /// </summary>
        /// <param name="id">La entidad que se desea eliminar.</param>
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var tipoAporte = contexto.TiposAportes.Find(id);

                if (tipoAporte != null)
                {
                    contexto.TiposAportes.Remove(tipoAporte);
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
        /// Permite obtener una lista de una entidad en la base de datos.
        /// </summary>
        public static List<TiposAportes> GetTiposAportes()
        {
            List<TiposAportes> lista = new List<TiposAportes>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.TiposAportes.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        /// <summary>
        /// Permite listar una entidad en la base de datos.
        /// </summary>
        /// <param name="criterio">La entidad que se desea listar.</param>
        public static List<TiposAportes> GetList(Expression<Func<TiposAportes, bool>> criterio)
        {
            List<TiposAportes> lista = new List<TiposAportes>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.TiposAportes.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
