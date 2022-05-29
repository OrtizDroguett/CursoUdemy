using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoUdemy.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeMV> lista = new List<ViajeMV>();
            using (var bd = new BDPasajeEntities())
            {
                listarComboBox();
                lista = (from viaje in bd.Viaje
                         join origen in bd.Lugar
                         on viaje.IIDLUGARORIGEN equals origen.IIDLUGAR
                         join destino in bd.Lugar
                         on viaje.IIDLUGARDESTINO equals destino.IIDLUGAR
                         join bus in bd.Bus
                         on viaje.IIDBUS equals bus.IIDBUS
                         where viaje.BHABILITADO == 1
                         select new ViajeMV
                         {
                             iidviaje=viaje.IIDVIAJE,
                             nombreOrigen = origen.NOMBRE,
                             nombreDestino = destino.NOMBRE,
                             precio = (decimal)viaje.PRECIO,
                             fechaviaje = (DateTime)viaje.FECHAVIAJE,
                             nombreBus=bus.PLACA,
                             numeroasientosdisponibles = (int)viaje.NUMEROASIENTOSDISPONIBLES
                         }).ToList();
            }

            return View(lista);
        }
        public ActionResult FiltroTabla(string nombreFiltro)
        {
            List<ViajeMV> Lista = new List<ViajeMV>();
            using (var bd = new BDPasajeEntities())
            {
                if (nombreFiltro == null)
                {
                    Lista = (from viaje in bd.Viaje
                             join origen in bd.Lugar
                             on viaje.IIDLUGARORIGEN equals origen.IIDLUGAR
                             join destino in bd.Lugar
                             on viaje.IIDLUGARDESTINO equals destino.IIDLUGAR
                             join bus in bd.Bus
                             on viaje.IIDBUS equals bus.IIDBUS
                             where viaje.BHABILITADO == 1
                             select new ViajeMV
                             {
                                 iidviaje = viaje.IIDVIAJE,
                                 nombreOrigen = origen.NOMBRE,
                                 nombreDestino = destino.NOMBRE,
                                 precio = (decimal)viaje.PRECIO,
                                 fechaviaje = (DateTime)viaje.FECHAVIAJE,
                                 nombreBus = bus.PLACA,
                                 numeroasientosdisponibles = (int)viaje.NUMEROASIENTOSDISPONIBLES
                             }).ToList();
                }
                else {
                    Lista = (from viaje in bd.Viaje
                             join origen in bd.Lugar
                             on viaje.IIDLUGARORIGEN equals origen.IIDLUGAR
                             join destino in bd.Lugar
                             on viaje.IIDLUGARDESTINO equals destino.IIDLUGAR
                             join bus in bd.Bus
                             on viaje.IIDBUS equals bus.IIDBUS
                             where viaje.BHABILITADO == 1
                             && bus.PLACA.Contains(nombreFiltro)
                             select new ViajeMV
                             {
                                 iidviaje = viaje.IIDVIAJE,
                                 nombreOrigen = origen.NOMBRE,
                                 nombreDestino = destino.NOMBRE,
                                 precio = (decimal)viaje.PRECIO,
                                 fechaviaje = (DateTime)viaje.FECHAVIAJE,
                                 nombreBus = bus.PLACA,
                                 numeroasientosdisponibles = (int)viaje.NUMEROASIENTOSDISPONIBLES
                             }).ToList();
                }
                
            }
            return PartialView("_TablaViaje", Lista);
        }
        private void listarLugar() {
            List<SelectListItem> Lista = new List<SelectListItem>();
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from lista in bd.Lugar
                         where lista.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = lista.NOMBRE,
                             Value = lista.IIDLUGAR.ToString()
                         }).ToList();
            }
            Lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            ViewBag.listarLugar = Lista;

        }
        private void listarBus() {
            List<SelectListItem> Lista = new List<SelectListItem>();
            using (var bd = new BDPasajeEntities())
            {
                Lista = (from lista in bd.Bus
                         where lista.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = lista.PLACA,
                             Value = lista.IIDBUS.ToString()
                         }).ToList();
            }
            Lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            ViewBag.listarBus = Lista;
        }
        private void listarComboBox() {
            listarLugar();
            listarBus();
        }
    }
}