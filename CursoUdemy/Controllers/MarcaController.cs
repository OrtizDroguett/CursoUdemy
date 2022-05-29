using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoUdemy.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index(MarcaMV marcaMV)
        {
            List<MarcaMV> Lista = new List<MarcaMV>();
            using (var bd = new BDPasajeEntities())
            {
                if (marcaMV.nombre == null)
                {
                    Lista = (from db in bd.Marca
                             where db.BHABILITADO == 1
                             select new MarcaMV
                             {
                                 iidmarca = db.IIDMARCA,
                                 nombre = db.NOMBRE,
                                 descripcion = db.DESCRIPCION
                             }).ToList();
                }
                else {
                    Lista = (from db in bd.Marca
                             where db.BHABILITADO == 1
                             && db.NOMBRE.Contains(marcaMV.nombre)
                             select new MarcaMV
                             {
                                 iidmarca = db.IIDMARCA,
                                 nombre = db.NOMBRE,
                                 descripcion = db.DESCRIPCION
                             }).ToList();
                }
                
            }
            return View(Lista);
        }
    }
}