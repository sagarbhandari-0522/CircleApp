using CircleApp.Data.Models;
using CircleApp.ViewModels;
using CircleApp.Constants;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CircleApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Provided Email is not Registered");
                return View(model);
            }
            var existingClaims = await _userManager.GetClaimsAsync(user);
            var existingClaim = existingClaims.FirstOrDefault(c => c.Type == CustomClaimTypes.FullName);
            if (existingClaim != null)
            {
                await _userManager.RemoveClaimAsync(user, existingClaim);
            }
            await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.FullName, user.FullName ?? ""));
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FullName = $"{model.FirstName} {model.LastName}",
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true
            };
            var existingUser = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered");
                return View(model);
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                var existingClaims = await _userManager.GetClaimsAsync(user);
                var existingClaim = existingClaims.FirstOrDefault(c => c.Type == CustomClaimTypes.FullName);
                if (existingClaim != null)
                {
                    await _userManager.RemoveClaimAsync(user, existingClaim);
                }
                await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.FullName, user.FullName ?? ""));
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid Request";
                return RedirectToAction("Index", "Setting");
            }
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                TempData["ErrorMessage"] = "Please log in to update profile";
                return RedirectToAction("Login");
            }
            loggedInUser.FullName = model.FullName;
            loggedInUser.UserName = model.UserName;
            loggedInUser.Bio = model.Bio;
            var updateResult = await _userManager.UpdateAsync(loggedInUser);
            if (updateResult.Succeeded)
            {
                TempData["SuccessMessage"] = "User updated successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "User Update Failed";
            }
            return RedirectToAction("Index", "Setting");
        }

        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action("ExternalLoginResponse", "Authentication");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        public async Task<IActionResult> ExternalLoginResponse()
        {
            User user;
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                TempData["ErrorMessage"] = "Error authenticating with Google";
                return RedirectToAction("Login");
            }
            if (info.LoginProvider == "Google")
                 user = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
            else
                 user = await _userManager.FindByNameAsync(info.Principal.FindFirstValue(ClaimTypes.Name));
            if (user == null)
            {
                user = new User
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)?? "",
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email)?? info.Principal.FindFirstValue(ClaimTypes.Name),
                    FullName = info.Principal.FindFirstValue(ClaimTypes.Name) ?? "",
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "User");

                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = "Error Creating User";
                    return RedirectToAction("login");

                }
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");


        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
