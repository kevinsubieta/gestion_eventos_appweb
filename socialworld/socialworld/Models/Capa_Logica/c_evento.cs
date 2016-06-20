using socialworld.Models.Capa_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace socialworld.Models.Capa_Logica
{
    public class c_evento
    {
        public List<evento> Get_All(int cliente)
        {
            return new EVENTOSBDEntities().eventoes.Where(x => x.idc == cliente).OrderByDescending(x => x.fechainicio).ToList();
        }

        public evento Get(int cliente, int id)
        {
            var query = new EVENTOSBDEntities().eventoes.Where(x => x.id == id && x.idc == cliente).ToList();
            if (query == null || query.Count < 1) return null;
            return query[0];
        }

        public void Registrar(string nombre, string direccion, long fechainicio, long fechafinal, int idc,int[] invitados)
        {
            using (var db = new EVENTOSBDEntities())
            {
                var query = new EVENTOSBDEntities().tarifas.FirstOrDefault();
                decimal precio = 1;
                if (query != null)
                {
                    precio = query.evento;
                }

                evento aux = new evento()
                {
                    nombre = nombre,
                    precio = precio,
                    direccion = direccion,
                    fechainicio = fechainicio,
                    fechafinal = fechafinal,
                    cancelado = false,
                    idc = idc,
                };
                
                foreach (var n in invitados)
                {
                    var query1 = db.contactoes.Where(x => x.id == n).ToList();
                    if (query1 != null && query1.Count > 0)
                    {
                        aux.contactoes.Add(query1[0]);
                    }
                }

                db.eventoes.Add(aux);
                db.SaveChanges();
            }
        }

        public void Actualizar(int id, string nombre, string direccion, long fechainicio, long fechafinal, bool cancelado, int[] add, int[] rem)
        {
            using (var db = new EVENTOSBDEntities())
            {
                var query = db.eventoes.Where(x => x.id == id).ToList();

                if (query != null && query.Count > 0)
                {
                    query[0].nombre = nombre;
                    query[0].direccion = direccion;
                    query[0].fechainicio = fechainicio;
                    query[0].fechafinal = fechafinal;
                    query[0].cancelado = cancelado;

                    foreach (int n in add)
                    {
                        var query1 = db.contactoes.Where(x => x.id == n).ToList();
                        if (query1 != null && query1.Count > 0)
                        {
                            query[0].contactoes.Add(query1[0]);
                        }
                    }

                    foreach (int k in rem)
                    {
                        var query2 = query[0].contactoes.Where(x => x.id == k).ToList();
                        if (query2 != null && query2.Count > 0)
                        {
                            query[0].contactoes.Remove(query2[0]);
                        }
                    }

                    db.SaveChanges();
                }
            }
        }

        private bool Validar(string nombre)
        {
            if (nombre == null || nombre.Length > 50) return false;
            return true;
        }
    }
}