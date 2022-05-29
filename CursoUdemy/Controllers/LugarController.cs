using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoUdemy.Controllers
{
    public class LugarController : Controller
    {
        // GET: Lugar
        public ActionResult Index()
        {
            List<LugarMV> Lista = null;
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from lista in bd.Lugar
                         where lista.BHABILITADO == 1
                         select new LugarMV
                         {
                             iidlugar = lista.IIDLUGAR,
                             nombre = lista.NOMBRE,
                             descripcion = lista.DESCRIPCION
                         }).ToList();
            }
            return View(Lista);
        }

        public ActionResult Filtro(string nombree)
        {
            List<LugarMV> Lista = new List<LugarMV>();
            using (var bd = new BDPasajeEntities()) {
                if (nombree == null)
                {
                    Lista = (from lista in bd.Lugar
                             where lista.BHABILITADO == 1
                             select new LugarMV
                             {
                                 iidlugar = lista.IIDLUGAR,
                                 nombre = lista.NOMBRE,
                                 descripcion = lista.DESCRIPCION
                             }).ToList();
                }
                else {
                    Lista = (from lista in bd.Lugar
                             where lista.BHABILITADO == 1
                             && lista.NOMBRE.Contains(nombree)
                             select new LugarMV
                             {
                                 iidlugar = lista.IIDLUGAR,
                                 nombre = lista.NOMBRE,
                                 descripcion = lista.DESCRIPCION
                             }).ToList();
                }
            }
                return PartialView("_TablaLugar", Lista);
        }

    }
}