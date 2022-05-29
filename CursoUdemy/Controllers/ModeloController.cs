using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoUdemy.Controllers
{
    public class ModeloController : Controller
    {
        // GET: Modelo
        public ActionResult Index()
        {
            List<ModeloMV> Lista = null;
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from lista in bd.Modelo
                         where lista.BHABILITADO == 1
                         select new ModeloMV
                         {
                             iidmodelo = lista.IIDMODELO,
                             nombre = lista.NOMBRE,
                             descripcion = lista.DESCRIPCION
                         }).ToList();
            }
            return View(Lista);
        }
        public ActionResult Filtro(ModeloMV modeloMV, string nombree)
        {
            List<ModeloMV> Lista = new List<ModeloMV>();
            using (var bd = new BDPasajeEntities())
            {
                if (nombree == null)
                {
                    Lista = (from lista in bd.Modelo
                             where lista.BHABILITADO == 1
                             select new ModeloMV
                             {
                                 iidmodelo = lista.IIDMODELO,
                                 nombre = lista.NOMBRE,
                                 descripcion = lista.DESCRIPCION
                             }).ToList();
                }
                else {
                    Lista = (from lista in bd.Modelo
                             where lista.BHABILITADO == 1
                             && lista.NOMBRE.Contains(nombree)
                             select new ModeloMV
                             {
                                 iidmodelo = lista.IIDMODELO,
                                 nombre = lista.NOMBRE,
                                 descripcion = lista.DESCRIPCION
                             }).ToList();
                }
                
                
            }
            
            
            return PartialView("_TablaModelo", Lista);
        }
        public string Agregar(ModeloMV modeloMV, int Titulo) {

            string Respuesta = "";
            if (!ModelState.IsValid)
            {
                var query = (from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage);
                Respuesta += "<ul>";
                foreach (var item in query)
                {
                    Respuesta += "<li class='text-danger'>"+item+"</li>";
                }
                Respuesta += "</ul>";
                
            }
            else { 
            if (Titulo == 1)
            {
                using (var bd = new BDPasajeEntities())
                {
                    Modelo modelo = new Modelo();
                    modelo.NOMBRE = modeloMV.nombre;
                    modelo.DESCRIPCION = modeloMV.descripcion;
                    modelo.BHABILITADO = 1;
                    bd.Modelo.Add(modelo);
                    Respuesta=bd.SaveChanges().ToString();
                }
            }
            }
            return Respuesta;
        }
    }
}