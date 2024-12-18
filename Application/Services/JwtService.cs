using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Application.Services
{
    public class JwtService
    {
        private readonly string _secretKey;
        private readonly IUserDapperRepository _userDapperRepository;

        public JwtService(IConfiguration configuration, IUserDapperRepository userDapperRepository)
        {
            _secretKey = configuration["Jwt:Key"]; 
            _userDapperRepository = userDapperRepository;
        }

        public string GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AgendaAPI",
                audience: "AgendaAPI",
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            token.Payload["username"] = username;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "AgendaAPI",
                    ValidAudience = "AgendaAPI",
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch
            {
                return null;
            }
        }
        public object SearchUser(string username, string password)
        {
            var user = _userDapperRepository.GetCredentialsUser(username, password);
            if (user == null)
            {
                throw new InvalidOperationException("No User Found! ");
            }
            return user;
        }
    }
}
