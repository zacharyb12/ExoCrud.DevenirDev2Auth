namespace ExoCrud.DevenirDev2.Models.Entites
{
    public class User
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }
    }
}
