using ChargeMe.Shared.Models;

namespace ChargeMe.BackEnd.Services.UserServices
{
    public interface IUserServices
    {
        public Task<User> GetUserById(string id);
        public Task<User> GetUserByEmail(string email);

        public Task<User> CreateUser(User user);
    }
}
