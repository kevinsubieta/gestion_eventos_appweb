//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace socialworld.Models.Capa_Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class contacto
    {
        public contacto()
        {
            this.eventoes = new HashSet<evento>();
        }
    
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public decimal precio { get; set; }
        public bool baja { get; set; }
        public int idc { get; set; }
    
        public virtual cliente cliente { get; set; }
        public virtual ICollection<evento> eventoes { get; set; }
    }
}