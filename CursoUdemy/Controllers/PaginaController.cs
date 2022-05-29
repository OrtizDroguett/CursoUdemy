using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoUdemy.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {
            List<PaginaMV> Lista = null;
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from lista in bd.Pagina
                         where lista.BHABILITADO == 1
                         select new PaginaMV
                         {
                             iidpagina = lista.IIDPAGINA,
                             accion = lista.ACCION,
                             controlador = lista.CONTROLADOR,
                             mensaje = lista.MENSAJE
                         }).ToList();
            }
            return View(Lista);
        }

        public ActionResult Filtro(string nombree)
        {
            List<PaginaMV> Lista = new List<PaginaMV>();
            using (var bd = new BDPasajeEntities())
            {
                if (nombree == null)
                {
                    Lista = (from lista in bd.Pagina
                             where lista.BHABILITADO == 1
                             select new PaginaMV
                             {
                                 iidpagina = lista.IIDPAGINA,
                                 accion = lista.ACCION,
                                 controlador = lista.CONTROLADOR,
                                 mensaje = lista.MENSAJE
                             }).ToList();
                }
                else
                {
                    Lista = (from lista in bd.Pagina
                             where lista.BHABILITADO == 1
                             && lista.MENSAJE.Contains(nombree)
                             select new PaginaMV
                             {
                                 iidpagina = lista.IIDPAGINA,
                                 accion = lista.ACCION,
                                 controlador = lista.CONTROLADOR,
                                 mensaje = lista.MENSAJE
                             }).ToList();
                }
            }
            return PartialView("_TablaPagina", Lista);
        }
        public string Agregar(PaginaMV paginaMV, int Titulo)
        {
            string Respuesta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    Respuesta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        Respuesta += "<li class='list-group-item text-danger'>" + item + "</li>";
                    }
                    Respuesta += "</ul>";


                }
                else
                {
                    if (Titulo == -1)
                    {
                        using (var bd = new BDPasajeEntities())
                        {
                            Pagina pagina = new Pagina();
                            pagina.MENSAJE = paginaMV.mensaje;
                            pagina.ACCION = paginaMV.accion;
                            pagina.CONTROLADOR = paginaMV.controlador;
                            pagina.BHABILITADO = 1;
                            bd.Pagina.Add(pagina);
                            Respuesta = bd.SaveChanges().ToString();
                            if (Respuesta == "0")
                            {
                                Respuesta = "";
                            }
                        }
                    }
                    else
                    {
                        using (var bd = new BDPasajeEntities())
                        {
                            Pagina pagina = bd.Pagina.Where(p => p.IIDPAGINA == Titulo).First();
                            pagina.MENSAJE = paginaMV.mensaje;
                            pagina.ACCION = paginaMV.accion;
                            pagina.CONTROLADOR = paginaMV.controlador;
                            Respuesta = bd.SaveChanges().ToString();
                            
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta = "";
            }

            return Respuesta;
        }
        public JsonResult recuperarDatos(int Titulo)
        {
            PaginaMV paginaMV = new PaginaMV();
            using (var bd = new BDPasajeEntities())
            {
                Pagina pagina = bd.Pagina.Where(p => p.IIDPAGINA == Titulo).First();
                paginaMV.mensaje = pagina.MENSAJE;
                paginaMV.accion = pagina.ACCION;
                paginaMV.controlador = pagina.CONTROLADOR;

            }
            return Json(paginaMV, JsonRequestBehavior.AllowGet);
        }
        public string Eliminar(int TituloEliminar) {
            string Respuesta = "";
            try
            {
                using (var bd = new BDPasajeEntities())
                {
                    Pagina pagina = bd.Pagina.Where(p => p.IIDPAGINA == TituloEliminar).First();
                    pagina.BHABILITADO = 0;
                    Respuesta = bd.SaveChanges().ToString();
                }
            }
            catch(Exception ex) {
                Respuesta = "";
            }
            return Respuesta;
        }
    }
}