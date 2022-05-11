using GestionPersonas.DAL;
using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestionPersonas.BLL
{
    public class RolesBLL
    {
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
                encontrado = contexto.Roles.Any(r => r.Descripcion.ToLower() == descripcion.ToLower());
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
        public static Roles Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Roles rol;

            try
            {
                rol = contexto.Roles.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return rol;
        }

        /// <summary>
        /// Permite guardar una entidad en la base de datos.
        /// </summary>
        /// <param name="rol">La entidad que se desea insertar y modificar la descripción.</param>
        public static bool Guardar(Roles rol)
        {
            if (!ExisteDescripcion(rol.Descripcion))
            {
                return Insertar(rol);
            }
            else
            {
                return Modificar(rol);
            }
        }

        /// <summary>
        /// Permite insertar una entidad en la base de datos.
        /// </summary>
        /// <param name="rol">La entidad que se desea insertar.</param>
        private static bool Insertar(Roles rol)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //Agregar la entidad que se desea insertar al contexto.
                contexto.Roles.Add(rol);
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
        /// <param name="rol">La entidad que se desea modificar.</param>
        public static bool Modificar(Roles rol)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(rol).State = EntityState.Modified;
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
                var rol = contexto.Roles.Find(id);

                if (rol != null)
                {
                    contexto.Roles.Remove(rol);
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
        public static List<Roles> GetRoles()
        {
            List<Roles> lista = new List<Roles>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Roles.ToList();
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
        public static List<Roles> GetList(Expression<Func<Roles, bool>> criterio)
        {
            List<Roles> lista = new List<Roles>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Roles.Where(criterio).ToList();
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
