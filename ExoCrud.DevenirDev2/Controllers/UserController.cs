using ExoCrud.DevenirDev2.Models.DTO.UserDTO;
using ExoCrud.DevenirDev2.Models.Entites;
using ExoCrud.DevenirDev2.Repository.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoCrud.DevenirDev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _service) : ControllerBase
    {
        [HttpGet]
        [Route("/{id:guid}")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                User? u = _service.GetUserById(id);

                if(u == null)
                {
                    return NotFound("User not found");
                }
                return Ok(u);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/update/{id:guid}")]
        public ActionResult UpdateUser(Guid id , UserUpdateDTO userUpdated)
        {
            try
            {
                bool result = _service.UpdateUser(userUpdated,id);

                if(!result)
                {
                    return NotFound("La modification n'as pas eu lieux");
                }

                return Ok("Utilisateur mis à jour avec succès");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("/delete/{id:guid}")]
        public ActionResult DeleteUser(Guid id)
        {
            try
            {
                bool success = _service.DeleteUser(id);

                if (!success)
                {
                    return BadRequest("Une erreur est survenur lors de la suppression");
                }

                return Ok("L'utilisateur à bien été supprimé");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
