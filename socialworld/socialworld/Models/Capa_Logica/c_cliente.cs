using socialworld.Models.Capa_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace socialworld.Models.Capa_Logica
{
    public class c_cliente
    {
        public cliente get(string user, string pass)
        {
            var query = new EVENTOSBDEntities().clientes.Where(x => x.usuario == user).ToList();
            if (query == null || query.Count() < 1 || encrypt(pass) != query[0].pass) return null;
            return query[0];
        }

        public string encrypt(string cadena)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Join("", hash.ComputeHash(Encoding.UTF8.GetBytes(cadena)).Select(item => item.ToString("x2")));
            }
        }
    }
}