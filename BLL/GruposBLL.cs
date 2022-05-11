using GestionPersonas.DAL;
using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestionPersonas.BLL
{
    public class GruposBLL
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
                encontrado = contexto.Grupos.Any(e => e.GrupoId == id);
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
        public static Grupos Buscar(int id)
        {
            Grupos grupo = new Grupos();
            Contexto contexto = new Contexto();

            try
            {
                grupo = contexto.Grupos.Include(x => x.GrupoDetalle)
                    .Where(x => x.GrupoId == id)
                    .Include(x => x.GrupoDetalle)
                    .ThenInclude(x => x.Persona)
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
            return grupo;
        }

        /// <summary>
        /// Permite Guardar una entidad en la base de datos.
        /// </summary>
        /// <param name="grupo">La entidad que se desea insertar y modificar.</param>
        public static bool Guardar(Grupos grupo)//si no existe insertamos.
        {
            if (!Existe(grupo.GrupoId))
                return Insertar(grupo);
            else
                return Modificar(grupo);
        }

        /// <summary>
        /// Permite Insertar una entidad en la base de datos.
        /// </summary>
        /// <param name="grupo">La entidad que se desea insertar.</param>
        private static bool Insertar(Grupos grupo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //Agregar la entidad que se desea insertar al contexto.
                contexto.Grupos.Add(grupo);

                foreach (var detalle in grupo.GrupoDetalle)
                {
                    detalle.Persona.CantidadGrupos += 1;
                }

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
        /// <param name="grupo">La entidad que se desea modificar.</param>
        private static bool Modificar(Grupos grupo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                 var grupoAnterior = contexto.Grupos
                    .Where(x => x.GrupoId == grupo.GrupoId)
                    .Include(x => x.GrupoDetalle)
                    .ThenInclude(x => x.Persona)
                    .AsNoTracking()
                    .SingleOrDefault();

                //Busca la entidad en la base de datos y la elimina.
                foreach (var detalle in grupoAnterior.GrupoDetalle)
                {
                    detalle.Persona.CantidadGrupos -= 1;
                }

                contexto.Database.ExecuteSqlRaw($"Delete FROM GruposDetalle Where GrupoId={grupo.GrupoId}");

                foreach (var item in grupo.GrupoDetalle)
                {
                    item.Persona.CantidadGrupos += 1;
                    contexto.Entry(item).State = EntityState.Added;
                }

                //Marcar la entidad como modificada para que el contexto sepa como proceder.
                contexto.Entry(grupo).State = EntityState.Modified;
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
                //Buscar la entidad que se desea eliminar.
                var grupo = GruposBLL.Buscar(id);

                if (grupo != null)
                {
                    //Busca la entidad en la base de datos y la elimina.
                    foreach (var detalle in grupo.GrupoDetalle)
                    {
                        detalle.Persona.CantidadGrupos -= 1;
                    }

                    contexto.Grupos.Remove(grupo);//Remover la entidad.
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
        public static List<Grupos> GetList(Expression<Func<Grupos, bool>> criterio)
        {
            List<Grupos> Lista = new List<Grupos>();
            Contexto contexto = new Contexto();

            try
            {
                //Obtener la lista y filtrarla según el criterio recibido por parametro.
                Lista = contexto.Grupos.Where(criterio).ToList();
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
