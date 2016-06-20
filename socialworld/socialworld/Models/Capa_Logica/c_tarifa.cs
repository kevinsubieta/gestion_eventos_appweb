using socialworld.Models.Capa_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace socialworld.Models.Capa_Logica
{
    public class c_tarifa
    {
        public decimal contacto()
        {
            var query = new EVENTOSBDEntities().tarifas.FirstOrDefault();
            if (query == null) return 1;
            return query.contacto;
        }

        public decimal evento()
        {
            var query = new EVENTOSBDEntities().tarifas.FirstOrDefault();
            if (query == null) return 1;
            return query.evento;
        }
    }
}