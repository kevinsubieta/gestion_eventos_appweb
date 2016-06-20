using socialworld.Models.Capa_Datos;
using socialworld.Models.Capa_Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace socialworld.Controllers
{
    public class EventosController : Controller
    {
        public ActionResult Index(int userid)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            ViewBag.eventos = new c_evento().Get_All(userid);
            ViewBag.precio = new c_tarifa().evento();
            return View();
        }

        public ActionResult eventovacio(int userid)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            ViewBag.contactos = new c_contacto().Get_All(userid);
            return View("_evento");
        }

        public ActionResult eventocompleto(int userid, int eventoid)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            ViewBag.evento = new c_evento().Get(userid, eventoid);
            ViewBag.contactos = new c_contacto().Get_All(userid);
            return View("_evento");
        }

        public ActionResult registrarevento(int userid, string nombre, string direccion, string fechainicio, string fechafinal, string[] add)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            new c_evento().Registrar(nombre, direccion, toepoch(fechainicio), toepoch(fechafinal), userid, tointarray(add));
            ViewBag.eventos = new c_evento().Get_All(userid);
            ViewBag.precio = new c_tarifa().evento();
            return View("Index");
        }

        public ActionResult actualizarevento(int userid, int eventoid, string nombre, string direccion, string fechainicio, string fechafinal, bool cancelado, string[] add, string[] rem)
        {
            if (!verif_log(userid)) return RedirectToAction("index", "home");
            new c_evento().Actualizar(eventoid, nombre, direccion, toepoch(fechainicio), toepoch(fechafinal), cancelado, tointarray(add), tointarray(rem));
            ViewBag.eventos = new c_evento().Get_All(userid);
            ViewBag.precio = new c_tarifa().evento();
            return View("Index");
        }

        private bool verif_log(int userid)
        {
            return ((Dictionary<int, cliente>)Session["clientes"]).ContainsKey(userid);
        }

        private long toepoch(string date)
        {
            return Convert.ToInt64((Convert.ToDateTime(date) - new DateTime(1970, 1, 1)).TotalSeconds);
        }

        private int[] tointarray(string[] valores)
        {
            if (valores != null && valores.Length > 0 && valores[0].Length > 0)
            {
                return Array.ConvertAll(valores, X => Convert.ToInt32(X));
            }
            return new int[0];
        }
    }
}
