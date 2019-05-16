using MentorIdentity.DALL.Models;
using MentorIdentity.DALL.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;


namespace MentorIdntity.BLL
{
    public class AccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager )
        {
            _userManager = userManager;
        }
        public async Task<RegisterResult> Register(RegisterViewModel model)
        {
            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.Email,
                DayOfBirth = model.DayOfBirth
            };
            RegisterResult result = new RegisterResult() { User = user }; 
            result.Identity = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
    }

    public class RegisterResult
    {
        public User User { get; set; }
        public IdentityResult Identity { get; set; }
    }
}
