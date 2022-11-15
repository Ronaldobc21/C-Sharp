//Este modelo servira para llenar los datos al momento del registro o llamar cierta informacion de un usuario
//Porque se va aplicar restrinciones al momento de hacer algun submit en los botones tanto de registrarse como de login
using System.ComponentModel.DataAnnotations;


namespace MVC.Models
{
    public class UsuarioModel
    {

        //Vamos a identificarlos
        [Key] //primarykey
        //Definimos propiedades
        public int Id_Usuario { get; set; }

        [Required(ErrorMessage = "Campo requerido")] // pues esto es para identificar cada unos de los campos y son requeridos
        public string? Nombre { get; set; } // se identifica que estas propiedades van a aceptar valores nulos
                                            // al final uno se asegura que no inserten valores nulos atraves de las validaciones. 

        [Required(ErrorMessage = "Campo requerido")]
        public string? Correo { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public int Edad { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public string? Usuario { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public string? Clave { get; set; }
    }
}


// Tenemos en UsuarioModel que va estar para almacenar los datos o valores  que se vayan a estar registrando o obtener la informacion de
// algun usuario que querramis obtener 