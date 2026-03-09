namespace ExoCrud.DevenirDev2.Models.DTO.CarDTO
{
    public class CreateCarDTO
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public int Horses { get; set; }

        public string Color { get; set; }

        public Guid UserId { get; set; }
    }
}
