using ExoCrud.DevenirDev2.Models.DTO.CarDTO;
using ExoCrud.DevenirDev2.Models.Entites;
using ExoCrud.DevenirDev2.Repository.CarServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExoCrud.DevenirDev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Nouvelle méthode d'injection de dépendance (C# 11)
    public class CarController(ICarService _carService) : ControllerBase
    {
        // Ancienne Méthode d'injection de dépendance
        //private readonly CarService _service;

        //public CarController(CarService service)
        //{
        //    _service = service;
        //}

        [HttpGet]
        [Route("/")]
        public ActionResult< IEnumerable<Car>> GetCars()
        {
            IEnumerable<Car> result = _carService.GetCarsService();

            return Ok(result);
        }



        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult<Car> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
               
                return BadRequest("L' id est incorrecte");
            }

            Car? response = _carService.GetById(id);

            if (response == null)
            {
                return NotFound("Aucun vehicule avec cet identifiant");
            }

            return Ok(response);
        }


       

        [HttpPost]
        [Route("/")]
        public ActionResult<bool> CreateCar([FromBody] CreateCarDTO newCar)
        {
            if (newCar == null)
            {
                return BadRequest("Les informations sont nécéssaires");
            }

            bool response = _carService.CreateCarService(newCar);

            if (!response)
            {
                return BadRequest("Le vehicule n'as pas pu être ajouté !");
            }

            return Ok(true);
        }

        [HttpPut]
        [Route("/")]
        public ActionResult<bool> UpdateCar([FromBody] UpdateCarDTO updatedCar, Guid id)
        {

            if (updatedCar == null)
            {
                return BadRequest("Les informations sur le vehicule sont incorrectes");
            }

            if (id == Guid.Empty)
            {
                return BadRequest("L'id est obligatoire");
            }

            bool response = _carService.UpdateCarService(updatedCar, id);

            if (!response)
            {
                return BadRequest("Une erreur est survenue lors de mise à jour ! ");
            }

            return Ok(true);

        }


        [HttpDelete]
        [Route("/{id:guid}")]
        public ActionResult<bool> DeleteCar(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("L'id est incorrecte");
            }

           bool response = _carService.DeleteCarService(id);

            if(!response)
            {
                return BadRequest("Une erreur est survenue pendant la suppression");
            }

            return Ok(true);
        }
    }
}


//[HttpGet]
//[Route("/body")]
//public ActionResult<Car> GetByIdBody([FromBody]Guid id)
//{
//    if (id == Guid.Empty)
//    {
//        return BadRequest("L' id est incorrecte");
//    }

//    Car? car = _cars.FirstOrDefault(c => c.Id == id);

//    if (car == null)
//    {
//        return NotFound("Aucun vehicule avec cet identifiant");
//    }

//    return Ok(car);
//}