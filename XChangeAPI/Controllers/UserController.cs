using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;
using XChangeAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using XChangeAPI.Controllers.Interfaces;

namespace XChangeAPI.Controllers
{

    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserLogic _ulogic;
        private readonly IConfiguration _conf;
        public UserController(ILogger<UserController> logger, IUserLogic uLogic, IConfiguration conf)
        {
            _ulogic = uLogic;
            _conf = conf;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> Register(UserRegisterDTO request)
        {
            var user = await _ulogic.GetUserByEmail(request.Username);
            if (user != null)
            {
                return BadRequest("User Exists");
            }

            string passHash = BCrypt.Net.BCrypt.HashPassword(request.Password, BCrypt.Net.BCrypt.GenerateSalt());
            user = new User
            {
                name = request.Name,
                surname = request.Surname,
                passwordhash = passHash,
                email = request.Username
            };

            var id = await _ulogic.PostUser(user);

            return new UserResponseDTO { ID = id, Token = CreateToken(user) };

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDTO>> Login(UserLoginDTO request)
        {
            var user = await _ulogic.GetUserByEmail(request.Username);
            if (user == null)
            {
                return BadRequest("Login Incorrect.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.passwordhash))
            {
                return BadRequest("Login Incorrect.");
            }

            return new UserResponseDTO { ID = user.id, Token = CreateToken(user) };
            ;

        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf.GetSection("AppSettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUser()
        {
            var Users = await _ulogic.GetUsers();
            return Users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {


            var history = await _ulogic.GetUserById(id);
            if (history == null)
            {
                throw new ArgumentException($"User with ID '{id}' not found");
            }
            return Ok(history);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(UserPut User)
        {
            if (User == null)
            {
                throw new ArgumentNullException(nameof(User));
            }
            var user = await _ulogic.GetUserById(User.id);
            user.modifiedOn = DateTime.UtcNow;
            user.email = User.email;
            user.name = User.name;
            user.surname = User.surname;
            user.status = User.status;
            if (!BCrypt.Net.BCrypt.Verify(User.password, user.passwordhash))
            {
                user.passwordhash = BCrypt.Net.BCrypt.HashPassword(User.password, BCrypt.Net.BCrypt.GenerateSalt());

            }

            await _ulogic.UpdateUser(user);
            return Ok(user);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //var User = await _dynamoDBContext.LoadAsync<User>(id);
            var history = await _ulogic.GetUserById(id);
            if (history == null)
            {
                throw new ArgumentException($"User with ID '{id}' not found");
            }
            await _ulogic.DeleteUser(history);
            return Ok();
        }
    }
}
