using XChangeAPI.Models.DB;

namespace XChangeAPI.Logic.Interfaces
{
    public interface IUserLogic
    {
        Task DeleteUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<int> PostUser(User user);
        Task UpdateUser(User user);
    }
}