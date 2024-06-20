using ChargeMe.Shared.Models;
using Microsoft.AspNetCore.Identity;


namespace ChargeMe.BackEnd.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private UserManager<User> _userManager;
        public UserServices(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
