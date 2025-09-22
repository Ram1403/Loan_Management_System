using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Loan_Management_System.Models;
using Loan_Management_System.Repository;
using Microsoft.IdentityModel.Tokens;

namespace Loan_Management_System.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repos;
        private readonly IConfiguration _config;
        public AuthService(IAuthRepository repos, IConfiguration config)
        {
            _repos = repos;
            _config = config;
        }
        LoginResponseViewModel IAuthService.Login(LoginViewModel login)
        {
            var response = _repos.Login(login);
            if (response.IsSuccess)
            {
                response.Token = GenerateToken(response.User);
                return response;
            }
            return response;
        }



        public string GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"])); // no space after :

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.ToString()),
        new Claim("Phone", user.PhoneNumber),
        new Claim("Role",user.Role.ToString()) ,// Custom claim
        new Claim("UserId", user.UserId.ToString()) ,// Custom claim
        new Claim("UserName", user.Username), // Custom claim
        new Claim("UserEmail", user.Email) // Custom claim

    };

            var tokenOptions = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],     
                audience: _config["JwtSettings:Audience"], // no space
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


    }
}
