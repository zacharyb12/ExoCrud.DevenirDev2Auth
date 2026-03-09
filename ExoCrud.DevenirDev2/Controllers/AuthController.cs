using ExoCrud.DevenirDev2.Models.DTO.AuthDTO;
using ExoCrud.DevenirDev2.Repository.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoCrud.DevenirDev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _service) : ControllerBase
    {

        [HttpPost]
        [Route("/register")]
        public ActionResult Register(RegisterDTO registerForm)
        {
            try
            {
                if(registerForm == null)
                {
                    return BadRequest("Le formulaire d'inscription est vide");
                }

                string? token = _service.Register(registerForm);

                if (token == null)
                {
                    return BadRequest("Les informations de connexion sont incorrectes");
                }

                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/login")]
        public ActionResult Login(LoginDTO loginForm)
        {
            try
            {
                if(loginForm == null)
                {
                    return BadRequest("Le formulaire de connexion est vide");
                }

                string? token = _service.Login(loginForm);

                if(token == null)
                {
                    return BadRequest("Les informations de connexion sont incorrectes");
                }

                return Ok(token);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
