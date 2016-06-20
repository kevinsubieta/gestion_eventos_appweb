using socialworld.Models.Capa_Datos;
using socialworld.Models.Capa_Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace socialworld.Controllers
{
    public class ContactosController : Controller
    {
        public ActionResult Index(int userid)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            ViewBag.contactos = new c_contacto().Get_All(userid);
            ViewBag.precio = new c_tarifa().contacto();
            return View();
        }

        public ActionResult contactovacio(int userid)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            return View("_contacto");
        }

        public ActionResult contactocompleto(int userid, int contactoid)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            ViewBag.contacto = new c_contacto().Get(userid, contactoid);
            return View("_contacto");
        }

        public ActionResult registrarcontacto(int userid, string nombres, string apellidos, string correo)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            new c_contacto().Registrar(nombres, apellidos, correo);
            ViewBag.contactos = new c_contacto().Get_All(userid);
            ViewBag.precio = new c_tarifa().contacto();
            return View("Index");
        }

        public ActionResult actualizarcontacto(int userid, int contactoid, string nombres, string apellidos, string correo, bool baja)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            new c_contacto().Actualizar(contactoid, nombres, apellidos, correo, baja);
            ViewBag.contactos = new c_contacto().Get_All(userid);
            ViewBag.precio = new c_tarifa().contacto();
            return View("Index");
        }

        private bool verif_log(int userid)
        {
            return ((Dictionary<int, cliente>)Session["clientes"]).ContainsKey(userid);
        }
    }
}
