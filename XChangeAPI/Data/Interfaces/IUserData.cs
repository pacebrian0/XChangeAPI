using XChangeAPI.Models.DB;

namespace XChangeAPI.Data.Interfaces
{
    public interface IUserData
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);

    }
}
