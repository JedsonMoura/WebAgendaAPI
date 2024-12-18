using Application.Dtos;
using Domain.Interfaces;
using Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserDapperRepository _userDapperRepository;
        private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;

        public AuthController(IConfiguration configuration, IPasswordHasher<Domain.Entities.User> passwordHasher, IUserDapperRepository userDapperRepository)
        {
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _userDapperRepository = userDapperRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {

            var user = await _userDapperRepository.GetCredentialsUser(request.Username, request.Password);

            if (user == null)
                return Unauthorized("Unauthorized : Invalid username or password!");
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result == PasswordVerificationResult.Success)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized("Unauthorized : Invalid username or password! ");
        }


        private string GenerateJwtToken(Domain.Entities.User user)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], 
                audience: _configuration["Jwt:Audience"],
                claims: claims, 
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }

}
