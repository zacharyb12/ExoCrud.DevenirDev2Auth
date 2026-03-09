namespace ExoCrud.DevenirDev2.Models.Entites
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Horses { get; set; }

        public string Color { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
