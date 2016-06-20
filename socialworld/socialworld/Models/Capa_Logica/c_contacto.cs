using socialworld.Models.Capa_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace socialworld.Models.Capa_Logica
{
    public class c_contacto
    {
        public List<contacto> Get_All(int idcliente)
        {
            return new EVENTOSBDEntities().contactoes.Where(x => x.idc == idcliente).OrderBy(x => x.nombres).ToList();
        }

        public contacto Get(int idcliente, int id)
        {
            var query = new EVENTOSBDEntities().contactoes.Where(x => x.id == id && x.idc == idcliente).ToList();
            if (query == null || query.Count < 1) return null;
            return query[0];
        }

        public void Registrar(string nombres, string apellidos, string correo)
        {
            using (var db = new EVENTOSBDEntities())
            {
                var query = new EVENTOSBDEntities().tarifas.FirstOrDefault();
                decimal precio = 1;
                if (query != null)
                {
                    precio = query.contacto;
                }
                
                db.contactoes.Add(new contacto()
                {
                    nombres = nombres,
                    apellidos = apellidos,
                    correo = correo,
                    precio = precio ,
                    baja = false
                });
                db.SaveChanges();
            }
        }

        public void Actualizar(int id, string nombres, string apellidos, string correo, bool baja)
        {
            using (var db = new EVENTOSBDEntities())
            {
                var query = db.contactoes.Where(x => x.id == id).ToList();
                if (query != null && query.Count > 0)
                {
                    query[0].nombres = nombres;
                    query[0].apellidos = apellidos;
                    query[0].correo = correo;
                    query[0].baja = baja;
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