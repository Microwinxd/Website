using BeanScene1._1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using static BeanScene1._1.ViewModels.AdminViewModels;

namespace BeanScene1._1.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = roles
                });
            }
            return View(model);
        }

            public async Task<IActionResult> EditRoles(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return NotFound();

                var model = new EditRolesViewModel
                {
                UserId = user.Id,
                Email = user.Email,
                Roles = new List<RoleCheckbox>()
                };
                var allRoles = await _roleManager.Roles.ToListAsync();
                foreach (var role in allRoles)
                {
                    model.Roles.Add(new RoleCheckbox
                    {
                    RoleName = role.Name,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                    });
                }
                return View(model);
            }

        [HttpPost]
        public async Task<IActionResult> EditRoles(EditRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (role.IsSelected && !currentRoles.Contains(role.RoleName))
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                else if (!role.IsSelected && currentRoles.Contains(role.RoleName))
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            }
            return RedirectToAction(nameof(Index));
            

            
            
        }
    }
}
