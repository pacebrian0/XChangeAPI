using Microsoft.AspNetCore.Mvc;
using XChangeAPI.Models.DB;
using XChangeAPI.Models.DTO;

namespace XChangeAPI.Controllers.Interfaces
{
    public interface IUserController
    {
        Task<IActionResult> DeleteUser(int id);
        Task<IEnumerable<User>> GetUser();
        Task<ActionResult<User>> GetUserById(int id);
        Task<ActionResult<UserResponseDTO>> Login(UserLoginDTO request);
        Task<ActionResult<User>> PostUser(UserPost user);
        Task<ActionResult<UserResponseDTO>> Register(UserRegisterDTO request);
        Task<ActionResult<User>> UpdateUser(UserPut User);
    }
}