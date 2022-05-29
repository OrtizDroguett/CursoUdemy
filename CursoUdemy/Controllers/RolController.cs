using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoUdemy.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index(RolMV rolMV)
        {
            List<RolMV> Lista = null;
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from lista in bd.Rol
                         where lista.BHABILITADO == 1
                         select new RolMV
                         {
                             iidrol = lista.IIDROL,
                             nombre = lista.NOMBRE,
                             descripcion = lista.DESCRIPCION
                         }).ToList();
            }

            return View(Lista);
        }
        public ActionResult Filtro(string nombree)
        {
            List<RolMV> Lista = new List<RolMV>();
            using (var bd = new BDPasajeEntities())
            {
                if (nombree == null)
                {
                    Lista = (from lista in bd.Rol
                             where lista.BHABILITADO == 1
                             select new RolMV
                             {
                                 iidrol = lista.IIDROL,
                                 nombre = lista.NOMBRE,
                                 descripcion = lista.DESCRIPCION
                             }).ToList();
                }
                else
                {
                    Lista = (from lista in bd.Rol
                             where lista.BHABILITADO == 1
                             && lista.NOMBRE.Contains(nombree)
                             select new RolMV
                             {
                                 iidrol = lista.IIDROL,
                                 nombre = lista.NOMBRE,
                                 descripcion = lista.DESCRIPCION
                             }).ToList();
                }
            }
            return PartialView("_TablaRol", Lista);
        }
        public string Guardar(RolMV rolMV, int Titulo)
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
                    using (var bd = new BDPasajeEntities())
                    {
                        if (Titulo == -1)
                        {
                            Rol rol = new Rol();
                            rol.NOMBRE = rolMV.nombre;
                            rol.DESCRIPCION = rolMV.descripcion;
                            rol.BHABILITADO = 1;
                            bd.Rol.Add(rol);
                            Respuesta = bd.SaveChanges().ToString();
                            if (Respuesta == "0") { Respuesta = ""; }
                        }
                        else
                        {
                            Rol rol = bd.Rol.Where(p => p.IIDROL == Titulo).First();
                            rol.NOMBRE = rolMV.nombre;
                            rol.DESCRIPCION = rolMV.descripcion;
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
            RolMV rolMV = new RolMV();
            using (var bd = new BDPasajeEntities())
            {
                Rol rol = bd.Rol.Where(p => p.IIDROL == Titulo).First();
                rolMV.nombre = rol.NOMBRE;
                rolMV.descripcion = rol.DESCRIPCION;
            }
            return Json(rolMV, JsonRequestBehavior.AllowGet);
        }

        public int Eliminar(int idRol) {

            int Respuesta = 0;
            try
            {
                using (var bd = new BDPasajeEntities())
                {
                    Rol rol = bd.Rol.Where(p=>p.IIDROL==idRol).First();
                    rol.BHABILITADO = 0;
                    Respuesta= bd.SaveChanges();
                }
            }
            catch (Exception ex) {
                Respuesta = 0;
            }
            return Respuesta;
        }
    }
}