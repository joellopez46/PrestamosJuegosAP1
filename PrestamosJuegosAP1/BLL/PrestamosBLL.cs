using Microsoft.EntityFrameworkCore;
using PrestamosJuegosAP1.DAL;
using PrestamosJuegosAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PrestamosJuegosAP1.BLL
{
    public class PrestamosBLL
    {
        public static bool Guardar(Prestamos prestamo)
        {
            if (!Existe(prestamo.PrestamoId))
                return Insertar(prestamo);
            else
                return Modificar(prestamo);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Prestamos.Any(p => p.PrestamoId == id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Insertar(Prestamos prestamo)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Prestamos.Add(prestamo);
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Modificar(Prestamos prestamo)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM PrestamoDetalle Where PrestamoId={prestamo.PrestamoId}");
                foreach (var item in prestamo.Detalles)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                contexto.Entry(prestamo).State = EntityState.Modified;
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

        public static Prestamos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Prestamos prestamo;

            try
            {
                prestamo = contexto.Prestamos.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return prestamo;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Prestamos.Find(id);
                if (eliminar != null)
                {
                    contexto.Entry(eliminar).State = EntityState.Deleted;
                    ok = contexto.SaveChanges() > 0;
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

            return ok;
        }

        public static List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Prestamos> lista = new List<Prestamos>();

            try
            {
                lista = contexto.Prestamos.Where(criterio).ToList();
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
