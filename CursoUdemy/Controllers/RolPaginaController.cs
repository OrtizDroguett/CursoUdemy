using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoUdemy.Controllers
{
    public class RolPaginaController : Controller
    {
        // GET: RolPagina
        public ActionResult Index()
        {
            listarComboBox();
            List<RolPaginaMV> Lista = null;
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from rolpagina in bd.RolPagina
                         join rol in bd.Rol
                         on rolpagina.IIDROL equals rol.IIDROL
                         join pagina in bd.Pagina
                         on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                         where rolpagina.BHABILITADO == 1
                         select new RolPaginaMV
                         {
                             iidrol = (int)rolpagina.IIDROL,
                             nombrePagina = pagina.MENSAJE,
                             nombreRol = rol.NOMBRE,
                             iidrolpagina = rolpagina.IIDROLPAGINA
                         }).ToList();
            }
            return View(Lista);
        }

        private void rellenarRol()
        {
            List<SelectListItem> Lista;
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from rol in bd.Rol
                         where rol.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = rol.NOMBRE,
                             Value = rol.IIDROL.ToString()
                         }).ToList();
            }
            Lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
            ViewBag.ListaRol = Lista;
        }
        private void rellenarPagina()
        {
            List<SelectListItem> Lista;
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from pag in bd.Pagina
                         where pag.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = pag.MENSAJE,
                             Value = pag.IIDPAGINA.ToString()
                         }).ToList();
            }
            Lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
            ViewBag.ListaPagina = Lista;
        }
        private void listarComboBox()
        {
            rellenarRol();
            rellenarPagina();
        }
        public ActionResult Filtro(int? iidrolFiltrar)
        {
            List<RolPaginaMV> Lista = new List<RolPaginaMV>();
            using (var bd = new BDPasajeEntities())
            {
                if (iidrolFiltrar == null)
                {
                    Lista = (from rolpagina in bd.RolPagina
                             join rol in bd.Rol
                             on rolpagina.IIDROL equals rol.IIDROL
                             join pagina in bd.Pagina
                             on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                             where rolpagina.BHABILITADO == 1
                             select new RolPaginaMV
                             {
                                 iidrol = (int)rolpagina.IIDROL,
                                 nombrePagina = pagina.MENSAJE,
                                 nombreRol = rol.NOMBRE,
                                 iidrolpagina = rolpagina.IIDROLPAGINA
                             }).ToList();
                }
                else
                {
                    Lista = (from rolpagina in bd.RolPagina
                             join rol in bd.Rol
                             on rolpagina.IIDROL equals rol.IIDROL
                             join pagina in bd.Pagina
                             on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                             where rolpagina.BHABILITADO == 1
                             && rolpagina.IIDROL == iidrolFiltrar
                             select new RolPaginaMV
                             {
                                 iidrol = (int)rolpagina.IIDROL,
                                 nombrePagina = pagina.MENSAJE,
                                 nombreRol = rol.NOMBRE,
                                 iidrolpagina = rolpagina.IIDROLPAGINA
                             }).ToList();
                }
            }
            return PartialView("_TablaRolPagina", Lista);
        }

        public string Agregar(RolPaginaMV rolPaginaMV, int Titulo)
        {
            string Respuesta = "";
            if (!ModelState.IsValid)
            {
                var query = (from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage).ToList();
                Respuesta += "<ul>";
                foreach (var item in query)
                {
                    Respuesta += "<li>" + item + "</li>";
                }
                Respuesta += "</ul>";
            }
            else
            {
                if (Titulo == -1)
                {
                    using (var bd = new BDPasajeEntities())
                    {
                        RolPagina rolPagina = new RolPagina();
                        rolPagina.IIDPAGINA = rolPaginaMV.iidpagina;
                        rolPagina.IIDROL = rolPaginaMV.iidrol;
                        rolPagina.BHABILITADO = 1;
                        bd.RolPagina.Add(rolPagina);
                        Respuesta = bd.SaveChanges().ToString();
                        if (Respuesta == "0") { Respuesta = ""; }

                    }
                }
                else {
                    using (var bd = new BDPasajeEntities())
                    {
                        RolPagina rolPagina = bd.RolPagina.Where(p => p.IIDROLPAGINA == Titulo).First();
                        rolPagina.IIDPAGINA = rolPaginaMV.iidpagina;
                        rolPagina.IIDROL = rolPaginaMV.iidrol;
                        Respuesta= bd.SaveChanges().ToString();
                    }
                }
                
            }
            return Respuesta;
        }

        public JsonResult recuperarDatos(int Titulo) {
            RolPaginaMV rolPaginaMV = new RolPaginaMV();
            using (var bd = new BDPasajeEntities())
            {
                RolPagina rolPagina = bd.RolPagina.Where(p => p.IIDROLPAGINA == Titulo).First();
                rolPaginaMV.iidpagina = (int)rolPagina.IIDPAGINA;
                rolPaginaMV.iidrol = (int)rolPagina.IIDROL;
            }
            return Json(rolPaginaMV, JsonRequestBehavior.AllowGet);
        
        }

        public string Eliminar(int Titulo) {
            string Respuesta = "";
            try {
                using (var bd = new BDPasajeEntities())
                {
                    RolPagina rolPagina = bd.RolPagina.Where(p => p.IIDROLPAGINA == Titulo).First();
                    rolPagina.BHABILITADO = 0;
                    Respuesta = bd.SaveChanges().ToString();
                }
            }
            catch(Exception ex){
                Respuesta = "";
            }
            return Respuesta;
        }


    }
}