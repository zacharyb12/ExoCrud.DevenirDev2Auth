using ExoCrud.DevenirDev2.Models.DTO.AuthDTO;

namespace ExoCrud.DevenirDev2.Repository.AuthServices
{
    public interface IAuthService
    {
        string Login(LoginDTO loginForm);

        string Register(RegisterDTO registerForm);
    }
}
