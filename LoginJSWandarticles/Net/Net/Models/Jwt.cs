using System.Security.Claims;

namespace NetCoreYouTube.Models
{
    public class Jwt
    {
        public string Key { get; set; } // Aqui lo mandamos a llamar por medio de variables
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Subject { get; set; }


        // si es correcto o no es correcto si es enviado no enviado el token 
        public static dynamic validarToken(ClaimsIdentity identity) // identity es el parametro
        {
            try
            {// como detecto si el token es valido o no con un if

                if(identity.Claims.Count() == 0 )// si el identity en las propiedades claim no trae ningun claim

                {
                    return new
                    {
                        success = false,
                        message = "Verificar si estas enviando un token valido",
                        result = ""
                    };
                }
                //en caso tenga datos el claim
                // nesesito saber que usuario le pertenece al token identity que es el parametro

                var id = identity.Claims.FirstOrDefault(x => x.Type ==  "id").Value; // Que me obtenga el valor id de UsuarioController


                Usuario usuario = Usuario.DB().FirstOrDefault(x => x.idUsuario == id);
                return new
                {
                    success =true, 
                    message = "exito",
                    result = usuario
                };
            
            
            }
            catch(Exception ex) // si ocurre un error se captura aqui
            {
                return new
                {
                    success = false,
                    message = "Catch: "+ ex.Message,
                    result = ""
                };
            }
        }
    }
  
}
