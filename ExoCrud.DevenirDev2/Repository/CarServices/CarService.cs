using ExoCrud.DevenirDev2.Data;
using ExoCrud.DevenirDev2.Models.DTO.CarDTO;
using ExoCrud.DevenirDev2.Models.Entites;

namespace ExoCrud.DevenirDev2.Repository.CarServices
{
    public class CarService(ExoContext _context) : ICarService
    {

        public IEnumerable<Car> GetCarsService()
        {
            return _context.Cars.ToList();
        }

        public Car? GetById(Guid id)
        {
            Car? c = _context.Cars.FirstOrDefault(c => c.Id == id);

            if(c == null)
            {
                return null; 
            }

            return c;
        }

        public bool CreateCarService(CreateCarDTO newCar)
        {
            Car carToAdd = new()
            {
                Id = Guid.Empty,
                Brand = newCar.Brand,
                Model = newCar.Model,
                Horses = newCar.Horses,
                Color = newCar.Color
            };

            var carAdded = _context.Cars.Add(carToAdd);
            _context.SaveChanges();


            if(carAdded == null)
            {
                return false;
            }
            return true;
        }

        public bool UpdateCarService(UpdateCarDTO carUpdated , Guid id)
        {
                Car? carToUpdate = _context.Cars.FirstOrDefault(c => c.Id == id);

                if (carToUpdate == null)
                {
                    return false;
                }

                carToUpdate.Model = carUpdated.Model;
                carToUpdate.Brand = carUpdated.Brand;
                carToUpdate.Horses = carUpdated.Horses;
                carToUpdate.Color = carUpdated.Color;

                _context.SaveChanges();


                return true;
        }

        public bool DeleteCarService(Guid id)
        {
            Car? carTodelete = _context.Cars.FirstOrDefault(c => c.Id == id);

            if(carTodelete == null)
            {
                return false; 
            }


            _context.Cars.Remove(carTodelete);
            _context.SaveChanges();

            return true;
        }
    }
}