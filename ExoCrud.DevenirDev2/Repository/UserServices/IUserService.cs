using ExoCrud.DevenirDev2.Models.DTO.UserDTO;
using ExoCrud.DevenirDev2.Models.Entites;

namespace ExoCrud.DevenirDev2.Repository.UserServices
{
    public interface IUserService
    {
        User? GetUserById(Guid id);

        bool UpdateUser(UserUpdateDTO user , Guid id);

        bool DeleteUser(Guid id);
    }
}