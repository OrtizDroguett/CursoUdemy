using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Security.Cryptography;
using System.Text;

namespace CursoUdemy.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List<UsuarioMV> Lista = new List<UsuarioMV>();
            listarComboBox();
            using (var bd = new BDPasajeEntities())
            {
                List<UsuarioMV> ListaCliente = (from usuario in bd.Usuario
                                                join cliente in bd.Cliente
                                                on usuario.IID equals cliente.IIDCLIENTE
                                                join rol in bd.Rol
                                                on usuario.IIDROL equals rol.IIDROL
                                                where usuario.bhabilitado == 1
                                                && usuario.TIPOUSUARIO == "C"
                                                select new UsuarioMV
                                                {
                                                    iidusuario = usuario.IIDUSUARIO,
                                                    nombrePersona = cliente.NOMBRE + " " + cliente.APPATERNO + " " + cliente.APMATERNO,
                                                    nombreusuario = usuario.NOMBREUSUARIO,
                                                    nombreRol = rol.NOMBRE,
                                                    nombreTipoUsuario = "Cliente"
                                                }).ToList();
                List<UsuarioMV> ListaEmpleado = (from usuario in bd.Usuario
                                                 join empleado in bd.Empleado
                                                 on usuario.IID equals empleado.IIDEMPLEADO
                                                 join rol in bd.Rol
                                                 on usuario.IIDROL equals rol.IIDROL
                                                 where usuario.bhabilitado == 1
                                                 && usuario.TIPOUSUARIO == "E"
                                                 select new UsuarioMV
                                                 {
                                                     iidusuario = usuario.IIDUSUARIO,
                                                     nombrePersona = empleado.NOMBRE + " " + empleado.APPATERNO + " " + empleado.APMATERNO,
                                                     nombreusuario = usuario.NOMBREUSUARIO,
                                                     nombreRol = rol.NOMBRE,
                                                     nombreTipoUsuario = "Empleado"
                                                 }).ToList();

                Lista.AddRange(ListaCliente);
                Lista.AddRange(ListaEmpleado);
                Lista = Lista.OrderBy(p => p.iidusuario).ToList();
            }
            return View(Lista);
        }
        public ActionResult Filtro(string txtNombreFiltrado)
        {
            List<UsuarioMV> Lista = new List<UsuarioMV>();
            listarComboBox();
            using (var bd = new BDPasajeEntities())
            {
                if (txtNombreFiltrado == null)
                {
                    List<UsuarioMV> ListaCliente = (from usuario in bd.Usuario
                                                    join cliente in bd.Cliente
                                                    on usuario.IID equals cliente.IIDCLIENTE
                                                    join rol in bd.Rol
                                                    on usuario.IIDROL equals rol.IIDROL
                                                    where usuario.bhabilitado == 1
                                                    && usuario.TIPOUSUARIO== "C"
                                                    select new UsuarioMV
                                                    {
                                                        iidusuario = usuario.IIDUSUARIO,
                                                        nombrePersona = cliente.NOMBRE + " " + cliente.APPATERNO + " " + cliente.APMATERNO,
                                                        nombreusuario = usuario.NOMBREUSUARIO,
                                                        nombreRol = rol.NOMBRE,
                                                        nombreTipoUsuario = "Cliente"
                                                    }).ToList();
                    List<UsuarioMV> ListaEmpleado = (from usuario in bd.Usuario
                                                     join empleado in bd.Empleado
                                                     on usuario.IID equals empleado.IIDEMPLEADO
                                                     join rol in bd.Rol
                                                     on usuario.IIDROL equals rol.IIDROL
                                                     where usuario.bhabilitado == 1
                                                     && usuario.TIPOUSUARIO == "E"
                                                     select new UsuarioMV
                                                     {
                                                         iidusuario = usuario.IIDUSUARIO,
                                                         nombrePersona = empleado.NOMBRE + " " + empleado.APPATERNO + " " + empleado.APMATERNO,
                                                         nombreusuario = usuario.NOMBREUSUARIO,
                                                         nombreRol = rol.NOMBRE,
                                                         nombreTipoUsuario = "Empleado"
                                                     }).ToList();

                    Lista.AddRange(ListaCliente);
                    Lista.AddRange(ListaEmpleado);
                    Lista = Lista.OrderBy(p => p.iidusuario).ToList();
                }
                else {
                    List<UsuarioMV> ListaCliente = (from usuario in bd.Usuario
                                                    join cliente in bd.Cliente
                                                    on usuario.IID equals cliente.IIDCLIENTE
                                                    join rol in bd.Rol
                                                    on usuario.IIDROL equals rol.IIDROL
                                                    where usuario.bhabilitado == 1
                                                    && usuario.TIPOUSUARIO == "C"
                                                    && (cliente.NOMBRE.Contains(txtNombreFiltrado) ||
                                                    cliente.APPATERNO.Contains(txtNombreFiltrado) ||
                                                    cliente.APMATERNO.Contains(txtNombreFiltrado))
                                                    select new UsuarioMV
                                                    {
                                                        iidusuario = usuario.IIDUSUARIO,
                                                        nombrePersona = cliente.NOMBRE + " " + cliente.APPATERNO + " " + cliente.APMATERNO,
                                                        nombreusuario = usuario.NOMBREUSUARIO,
                                                        nombreRol = rol.NOMBRE,
                                                        nombreTipoUsuario = "Cliente"
                                                    }).ToList();
                    List<UsuarioMV> ListaEmpleado = (from usuario in bd.Usuario
                                                     join empleado in bd.Empleado
                                                     on usuario.IID equals empleado.IIDEMPLEADO
                                                     join rol in bd.Rol
                                                     on usuario.IIDROL equals rol.IIDROL
                                                     where usuario.bhabilitado == 1
                                                     && usuario.TIPOUSUARIO == "E"
                                                     && (empleado.NOMBRE.Contains(txtNombreFiltrado) ||
                                                     empleado.APPATERNO.Contains(txtNombreFiltrado) ||
                                                     empleado.APMATERNO.Contains(txtNombreFiltrado))
                                                     select new UsuarioMV
                                                     {
                                                         iidusuario = usuario.IIDUSUARIO,
                                                         nombrePersona = empleado.NOMBRE + " " + empleado.APPATERNO + " " + empleado.APMATERNO,
                                                         nombreusuario = usuario.NOMBREUSUARIO,
                                                         nombreRol = rol.NOMBRE,
                                                         nombreTipoUsuario = "Empleado"
                                                     }).ToList();

                    Lista.AddRange(ListaCliente);
                    Lista.AddRange(ListaEmpleado);
                    Lista = Lista.OrderBy(p => p.iidusuario).ToList();

                }
                

                
            }
            return PartialView("_TablaUsuario", Lista);
        }
        private void listarPersonas()
        {
            List<SelectListItem> listaEmpleado;
            List<SelectListItem> listaCliente;
            List<SelectListItem> lista = new List<SelectListItem>();
            using (var bd = new BDPasajeEntities())
            {
                listaEmpleado = (from empleado in bd.Empleado
                                 where empleado.BHABILITADO == 1
                                 select new SelectListItem
                                 {
                                     Text = empleado.NOMBRE + " " + empleado.APPATERNO + " " + empleado.APMATERNO + " (" + empleado.TIPOUSUARIO + ")",
                                     Value = empleado.IIDEMPLEADO.ToString()
                                 }).ToList();
                listaCliente = (from cliente in bd.Cliente
                                where cliente.BHABILITADO == 1
                                select new SelectListItem
                                {
                                    Text = cliente.NOMBRE + " " + cliente.APPATERNO + " " + cliente.APMATERNO + " (" + cliente.TIPOUSUARIO + ")",
                                    Value = cliente.IIDCLIENTE.ToString()
                                }).ToList();
                lista.AddRange(listaEmpleado);
                lista.AddRange(listaCliente);
                lista = lista.OrderBy(p => p.Text).ToList();
            }
            lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
            ViewBag.listaPersonas = lista;

        }

        private void listarRol()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from rol in bd.Rol
                         where rol.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = rol.NOMBRE,
                             Value = rol.IIDROL.ToString()
                         }).ToList();
            }
            lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            ViewBag.listaRol = lista;
        }
        private void listarComboBox()
        {
            listarPersonas();
            listarRol();
        }

        public int Guardar(UsuarioMV usuarioMV, int Titulo)
        {
            int Respuesta = 0;
            try
            {

                using (var bd = new BDPasajeEntities())
                {
                    using (var transaccion = new TransactionScope())//bloque de transacción
                    {
                        if (Titulo == 1)
                        {
                            Usuario usuario = new Usuario();
                            usuario.NOMBREUSUARIO = usuarioMV.nombreusuario;
                            SHA256Managed sha = new SHA256Managed();//esta clase nos va ayudar a cifrar la información
                            byte[] bytecontra = Encoding.Default.GetBytes(usuarioMV.contra);//Permite convertir una cadena a bytes, recuerda escribir "using system.text;"
                            byte[] bytecontraCifrado = sha.ComputeHash(bytecontra);//cifrar los bytes que hemos capturado
                            string cadenaContraCifrada = BitConverter.ToString(bytecontraCifrado).Replace("-", "");//convertirlo a cadena, a string, no vamos a guardar bytes  en la base de datos, guardaremos una cadena,
                                                                                                                   // reemplazamos el guión porque siempre la información cifrada tiene un guión separando
                            usuario.CONTRA = cadenaContraCifrada;
                            usuario.TIPOUSUARIO = usuarioMV.nombrePersona.Substring(usuarioMV.nombrePersona.Length - 2, 1);
                            usuario.IID = usuarioMV.iid;
                            usuario.IIDROL = usuarioMV.iidrol;
                            usuario.bhabilitado = 1;
                            bd.Usuario.Add(usuario);

                            if (usuario.TIPOUSUARIO.Equals("C"))
                            {
                                Cliente cliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(usuarioMV.iid)).First();
                                cliente.bTieneUsuario = 1;
                            }
                            else
                            {
                                Empleado empleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(usuarioMV.iid)).First();
                                empleado.bTieneUsuario = 1;
                            }
                            Respuesta = bd.SaveChanges();
                            transaccion.Complete();//al estar dentro del bloque transaccion, el savechanges pierde su poder, por lo que debes escribir esto, si no, no se veran los cambios
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta = 0;
            }
            return Respuesta;
        }
    }
}