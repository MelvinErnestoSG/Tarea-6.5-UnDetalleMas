using GestionPersonas.DAL;
using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestionPersonas.BLL
{
    public class PersonasBLL
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
                encontrado = contexto.Personas.Any(r => r.PersonaId == id);
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
        public static Personas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Personas persona;

            try
            {
                persona = contexto.Personas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return persona;
        }

        /// <summary>
        /// Permite guardar una entidad en la base de datos.
        /// </summary>
        /// <param name="persona">La entidad que se desea insertar y modificar.</param>
        public static bool Guardar(Personas persona)
        {
            if (!Existe(persona.PersonaId))
            {
                return Insertar(persona);
            }
            else
            {
                return Modificar(persona);
            }
        }

        /// <summary>
        /// Permite Insertar una entidad en la base de datos.
        /// </summary>
        /// <param name="persona">La entidad que se desea insertar.</param>
        private static bool Insertar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Personas.Add(persona);
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
        /// Permite Modificar una entidad en la base de datos.
        /// </summary>
        /// <param name="persona">La entidad que se desea modificar.</param>
        public static bool Modificar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(persona).State = EntityState.Modified;
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
                var persona = contexto.Personas.Find(id);

                if (persona != null)
                {
                    contexto.Personas.Remove(persona);
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
        public static List<Personas> GetPersonas()
        {
            List<Personas> lista = new List<Personas>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Personas.ToList();
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
        /// Permite Listar una entidad en la base de datos.
        /// </summary>
        /// <param name="criterio">La entidad que se desea listar.</param>
        public static List<Personas> GetList(Expression<Func<Personas, bool>> criterio)
        {
            List<Personas> lista = new List<Personas>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Personas.Where(criterio).ToList();
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
