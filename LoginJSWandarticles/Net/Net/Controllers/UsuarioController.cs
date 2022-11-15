using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using NetCoreYouTube.Models;





namespace NetCoreYouTube.Controllers
{

    [ApiController] // estan son sus rutas toda api debe tener unas rutas 
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {

        public IConfiguration _configuration; // Aqui en esta variable vamos obtener toda la configuracion que nesesitamos
        //esto es un constructor como sabemos que es constructor porque tiene que tener el mismo nombre que la clase 
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public dynamic IniciarSesion([FromBody] Object optData) // Los datos que va mandar el usuario va ser de tipo objeto
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());


            string user = data.usuario.ToString();
            string password = data.password.ToString(); // Esto es lo que escribimos dentro del potsman 
            // Vamos a obtener que usuario en base al Usuario 

            Usuario usuario = Usuario.DB().Where(x => x.usuario == user && x.password == password).FirstOrDefault();

            if (usuario == null)
            { // Si el usuario no existe en la DB va retornar esto
                return new
                {
                    sucess = false,
                    message = "Crendenciales incorrectas",
                    result = ""
                };
            }


            var jwt = _configuration.GetSection("Jwt").Get<Jwt>(); // Con getsection sirve que estamos obteniendo los datos del appseting.json en otras palabras del Json

            var claims = new[]//Aqui especificaremos todo lo que va incluir nuestro token
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.idUsuario),
                new Claim("usuario", usuario.usuario)

            }; 


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)); // Osea nuestra contraseña que esta en Jwt models que es Key tiene que convertirse en bytes
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(4),
                signingCredentials: singIn
                
                );


            return new
            {
                success = true,
                message = "exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)


            };

        }




    }
}