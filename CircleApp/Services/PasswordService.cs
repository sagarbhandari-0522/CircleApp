using CircleApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CircleApp.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly UserManager<User> _userManager;
        public PasswordService(UserManager<User> userManager )
        {
            _userManager = userManager;
        }
        public async Task<(bool Success,List<string> Errors)> UpdatePasswordAsync(string currentUserId, string currentPassword, string newPassword, string confirmPassword)
        {
            var errors = new List<string>();
            var user = await _userManager.FindByIdAsync(currentUserId);
            if(user==null)
            {
                errors.Add("User not found");
                return (false,  errors);
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, currentPassword);
            if(!isPasswordValid)
            {
                errors.Add("Current Password is not correct");
                return (false, errors);
            }
            var passwordUpdateResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if(passwordUpdateResult.Succeeded)
            {
                return (true, errors);
            }
            else
            {
                errors.AddRange(passwordUpdateResult.Errors.Select(e => e.Description));
                return (false, errors);
            }
        }
    }
}
