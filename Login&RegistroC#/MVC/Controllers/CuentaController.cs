using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace MVC.Controllers
{
    public class CuentaController : Controller
    {
        private readonly DbContext _contexto; // se agrego una variable privada de solo lectura

        public CuentaController(DbContext contexto)
        {
            _contexto = contexto; // se va a hacer una igualdad de los parametros

        }

        public ActionResult Registrar()
        {
            return View("Registrar");
        }

        [HttpPost]
        public ActionResult Registrar(UsuarioModel u) // El UsuarioModel tiene el valor de u, que se le da a este metodo
        {

            try
            {
                //Aaqui agregaremos los procedimientos almacenados
                //Validar que mi modelo usuario esta correcto
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new(_contexto.Valor))
                    {
                        using (SqlCommand cmd = new("sp_registrar", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure; //CommandType es un procedimiento almacenado
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = u.Nombre;
                            cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = u.Correo;
                            cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = u.Edad;
                            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = u.Usuario;
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar).Value = u.Clave;
                            con.Open(); // La abrimos 
                            cmd.ExecuteNonQuery(); // ejecutamos el procedimiento
                            con.Close();
                        }
                    }

                    return RedirectToAction("Index", "Home"); // Returnar la vista index de mi controlador home



                }

            }
            catch (Exception)
            {
                return View("Registrar");

            }

            ViewData["error"] = "Error de credenciales";
            return View("Registrar");

        }



        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginModel l)
        {
            try
            {
                //Aaqui agregaremos los procedimientos almacenados
                //Validar que mi modelo usuario esta correcto
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new(_contexto.Valor))
                    {
                        using (SqlCommand cmd = new("sp_login", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure; //CommandType es un procedimiento almacenado
                            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = l.Usuario;
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar).Value = l.Clave;
                            con.Open(); // La abrimos 

                            SqlDataReader dr = cmd.ExecuteReader(); // procedimiento almacenado 

                            if (dr.Read())
                            {
                                //en el caso que se obtengan valores vamos a redireccionar al usuario 
                                Response.Cookies.Append("user", "Bienvenido " + l.Usuario); // Al crear esta cookie se almacena solo un mensaje de bienvenida
                                //Se va crear una cookie donde se almacenara el valor de 
                                return RedirectToAction("Index", "Home");

                            }
                            else
                            {
                                ViewData["error"] = "Error de credenciales";
                            }


                            con.Close();
                        }
                    }

                    return RedirectToAction("Index", "Home"); // Returnar la vista index de mi controlador home



                }

            }
            catch (Exception)
            {
                return View("Login");

            }


            return View("Login");

        }



        public ActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index", "Home"); // A la vista Index del controlador Home
        }



    }
}
