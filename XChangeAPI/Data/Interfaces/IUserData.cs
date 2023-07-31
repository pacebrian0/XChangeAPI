using XChangeAPI.Models.DB;

namespace XChangeAPI.Data.Interfaces
{
    public interface IUserData
    {
        Task DeleteUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<int> PostUser(User user);
        Task UpdateUser(User user);
    }
}