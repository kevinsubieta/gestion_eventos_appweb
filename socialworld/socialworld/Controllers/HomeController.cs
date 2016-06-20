using socialworld.Models.Capa_Datos;
using socialworld.Models.Capa_Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace socialworld.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["clientes"] = new Dictionary<int, cliente>();
            return View();
        }

        public JavaScriptResult iniciarsesion(string user, string pass)
        {
            cliente aux = new c_cliente().get(user, pass);
            if (aux != null)
            {
                Dictionary<int, cliente> diccionario = (Dictionary<int, cliente>)Session["clientes"];
                if (!diccionario.ContainsKey(aux.id)) diccionario.Add(aux.id, aux);
                return JavaScript("entrar(" + aux.id + ");");
            }
            return JavaScript("Errorlogin();");
        }

        public ActionResult entrar(int userid)
        {
            if (!verif_log(userid)) return View("Index");
            return View("_principal");
        }

        public ActionResult salir(int userid)
        {
            ((Dictionary<int, cliente>)Session["clientes"]).Remove(userid);
            TempData["lay"] = true;
            return RedirectToAction("Index", "home");
        }

        private bool verif_log(int userid)
        {
            return ((Dictionary<int, cliente>)Session["clientes"]).ContainsKey(userid);
        }
    }
}
