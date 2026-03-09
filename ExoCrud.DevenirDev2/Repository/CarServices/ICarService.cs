using ExoCrud.DevenirDev2.Models.DTO.CarDTO;
using ExoCrud.DevenirDev2.Models.Entites;

namespace ExoCrud.DevenirDev2.Repository.CarServices
{
    public interface ICarService
    {
        IEnumerable<Car> GetCarsService();
        Car? GetById(Guid id);
        bool CreateCarService(CreateCarDTO newCar);
        bool UpdateCarService(UpdateCarDTO carToUpdate, Guid id);
        bool DeleteCarService(Guid id);
    }
}
