using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZalakProject.Models;
using ZalakProject.ViewModels;

namespace ZalakProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminUserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles.ToList(),
                    IsLocked = user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow
                });
            }

            return View(userViewModels);
        }

        public async Task<IActionResult> EditRoles(string id)
        {
            // Load user & roles to edit
            return View();
        }

        public async Task<IActionResult> ToggleLock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                user.LockoutEnd = DateTimeOffset.UtcNow;
            }
            else
            {
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100); // Lock forever
            }

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
