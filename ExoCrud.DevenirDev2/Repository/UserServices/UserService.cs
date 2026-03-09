using ExoCrud.DevenirDev2.Data;
using ExoCrud.DevenirDev2.Models.DTO.UserDTO;
using ExoCrud.DevenirDev2.Models.Entites;

namespace ExoCrud.DevenirDev2.Repository.UserServices
{
    public class UserService(ExoContext _context) : IUserService
    {
        public bool DeleteUser(Guid id)
        {
            User? u = _context.Users.FirstOrDefault(u => u.Id == id);

            if(u == null)
            {
                return false;
            }

            _context.Users.Remove(u);
            _context.SaveChanges();
            return true;
        }

        public User? GetUserById(Guid id)
        {
            User? u = _context.Users.FirstOrDefault(u => u.Id == id);

            if (u == null)
            {
                return null;
            }
            return u;
        }

        public bool UpdateUser(UserUpdateDTO user, Guid id)
        {
            User? u = _context.Users.FirstOrDefault(u => u.Id == id);

            if(u == null)
            {
                return false;
            }

            u.Firstname = user.Firstname;
            u.Lastname = user.Lastname;
            u.Email = user.Email;

            _context.SaveChanges();
            return true;
        }
    }
}
