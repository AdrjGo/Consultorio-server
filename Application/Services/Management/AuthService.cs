using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Responses;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }


        public async Task<AuthResponse> LoginUser(string email, string password)
        {

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return new AuthResponse { Success = false, ErrorMessage = "Correo incorrecto." };

            var emailValue = user.Person?.Email?.Value;

            if (string.IsNullOrEmpty(emailValue))
                throw new Exception("El usuario no tiene email asignado");


            if (user.State != States.ACTIVE)
                return new AuthResponse { Success = false, ErrorMessage = "El usuario está desactivado." };

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return new AuthResponse { Success = false, ErrorMessage = "Contraseña incorrecta." };


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("personId", user.Person.Id.ToString()),
                new Claim(ClaimTypes.Email, emailValue),
                new Claim("name", user.Person.Name + " " + user.Person.LastName),
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthResponse { Success = true, Token = tokenHandler.WriteToken(token), Time = tokenDescriptor.Expires.ToString() };
        }
    }
}