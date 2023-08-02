using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XChangeAPI.Data.Interfaces;
using XChangeAPI.Models.DB;
using XChangeAPI.Services.Interfaces;

namespace XChangeAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _conf;
        private readonly IUserData _data;
        public AuthService(IConfiguration conf, IUserData data)
        {
            _conf = conf;
            _data = data;
        }

        public Task<string> CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf.GetSection("AppSettings:JWTToken").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(jwt);
        }
        public async Task<string> HashPassword(string password)
        {
            try
            {
                return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<int?> ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_conf.GetSection("AppSettings:JWTToken").Value!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var email = jwtToken.Claims.First(x => x.Type == "email")?.Value ?? null;
                if (string.IsNullOrEmpty(email))
                {
                    return null;
                }

                var user = await _data.GetUserByEmail(email);

                if (user == null)
                {
                    return null;
                }
                // return user id from JWT token if validation successful
                return user.id;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
