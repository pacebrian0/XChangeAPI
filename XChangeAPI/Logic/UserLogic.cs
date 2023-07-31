using XChangeAPI.Data.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserData _data;
        private readonly ILogger<UserLogic> _log;

        public UserLogic(IUserData data, ILogger<UserLogic> log)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _data.GetUsers();

        }
        public async Task<User> GetUserById(int id)
        {
            return await _data.GetUserById(id);

        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _data.GetUserByEmail(email);
        }

        public async Task<int> PostUser(User user)
        {
            return await _data.PostUser(user);
        }

        public async Task UpdateUser(User user)
        {
            await _data.UpdateUser(user);
        }

        public async Task DeleteUser(User user)
        {
            await _data.DeleteUser(user);
        }
    }
}
