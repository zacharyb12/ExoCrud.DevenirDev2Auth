using ExoCrud.DevenirDev2.Data;
using ExoCrud.DevenirDev2.Models.DTO.AuthDTO;
using ExoCrud.DevenirDev2.Models.Entites;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExoCrud.DevenirDev2.Repository.AuthServices
{
    public class AuthService(ExoContext _context,IConfiguration _configuration) : IAuthService
    {
        public string Login(LoginDTO loginForm)
        {
            try
            {
                if(loginForm.Email == null || loginForm.Email == string.Empty)
                {
                    return null;
                }

                if (loginForm.Password == null || loginForm.Password == string.Empty)
                {
                    return null;
                }

                User? u = _context.Users.FirstOrDefault(u => u.Email == loginForm.Email);

                if (u == null)
                {
                    return null;
                }
                // Verification du mot de passe (hash) evec BCrypt
                if (!BCrypt.Net.BCrypt.Verify(loginForm.Password,u.PasswordHash))
                {
                    return null;
                }

                string token = GenerateToken(u);

                return token;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public string Register(RegisterDTO registerForm)
        {
            try
            {
                if(registerForm == null)
                {
                    return null;
                }

                if(_context.Users.Any(u => u.Email == registerForm.Email))
                {
                    return null;
                }

                User userToAdd = new()
                {
                    Id = Guid.Empty,
                    Firstname = registerForm.Firstname,
                    Lastname = registerForm.Lastname,
                    Email = registerForm.Email,
                    // Le mot de passe est hashé avec BCrypt avant d'être stocké dans la base de données
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerForm.Password)
                };

                _context.Users.Add(userToAdd);
                _context.SaveChanges();

                string token = GenerateToken(userToAdd);

                return token;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private string GenerateToken(User u)
        {
            // Recuperation de la secret key du fichier appsettings.json
            var secretKey = _configuration["Jwt:SecretKey"];
            var expirationHours = 1;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            // Creation de la configuration du token JWT
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // valeurs enregistrer dans le token
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub ,u.Id.ToString()),
                    new Claim(ClaimTypes.Role , u.Role),
                    new Claim(JwtRegisteredClaimNames.Name , $"{u.Firstname} {u.Lastname}"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() )

                }),
                Expires = DateTime.UtcNow.AddHours(expirationHours),
                SigningCredentials = new SigningCredentials(
                    // clé pour signer le token
                    new SymmetricSecurityKey(key),
                    // algorithme de signature du token
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            // Creation du token JWT
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Renvoie le token 
            return tokenHandler.WriteToken(token);
        }
    }
}
