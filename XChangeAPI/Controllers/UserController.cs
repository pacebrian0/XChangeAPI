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
using XChangeAPI.Services.Interfaces;

namespace XChangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserLogic _ulogic;
        private readonly IConfiguration _conf;
        private readonly IAuthService _auth;
        private readonly ILogger<UserController> _log;

        public UserController(ILogger<UserController> log, IAuthService auth, IUserLogic uLogic, IConfiguration conf)
        {

            _ulogic = uLogic ?? throw new ArgumentNullException(nameof(uLogic));
            _conf = conf ?? throw new ArgumentNullException(nameof(conf));
            _auth = auth ?? throw new ArgumentNullException(nameof(auth));
            _log = log ?? throw new ArgumentNullException(nameof(log));


        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> Register(UserRegisterDTO request)
        {
            var user = await _ulogic.GetUserByEmail(request.Email);
            if (user != null)
            {
                return BadRequest("User Exists");
            }

            string passHash = await _auth.HashPassword(request.Password);
            user = new User
            {
                name = request.Name,
                surname = request.Surname,
                passwordhash = passHash,
                email = request.Email
            };

            var id = await _ulogic.PostUser(user);

            return new UserResponseDTO { ID = id, Token = await _auth.CreateToken(user) };

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDTO>> Login(UserLoginDTO request)
        {
            var user = await _ulogic.GetUserByEmail(request.Email);
            if (user == null)
            {
                return BadRequest("Login Incorrect.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.passwordhash))
            {
                return BadRequest("Login Incorrect.");
            }

            return new UserResponseDTO { ID = user.id, Token = await _auth.CreateToken(user) };
            ;

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

            try
            {
                var user = await _ulogic.GetUserById(id);
                if (user == null)
                {
                    throw new ArgumentException($"User with ID '{id}' not found");
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserPost user)
        {
            try
            {
                await _ulogic.PostUser(new User
                {
                    id = 0,
                    email = user.email,
                    name = user.name,
                    surname = user.surname,
                    status = 'A',
                    createdOn = DateTime.UtcNow,
                    createdBy = 1,
                    modifiedBy = 1,
                    modifiedOn = DateTime.UtcNow,
                    passwordhash = ""

                });
                return Ok(user);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return BadRequest(e.Message);
            }


        }


        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(UserPut User)
        {
            if (User == null)
            {
                return BadRequest("Invalid Request");
            }

            try
            {
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
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return BadRequest(e.Message);
            }


        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _ulogic.GetUserById(id);
                if (user == null)
                {
                    throw new ArgumentException($"User with ID '{id}' not found");
                }
                await _ulogic.DeleteUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }
    }
}
