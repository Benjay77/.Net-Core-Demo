using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.Models;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Controllers
{
    [Authorize(Policy = "仅限管理员")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel addRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addRoleViewModel);
            }

            var role = new IdentityRole(){Name  = addRoleViewModel.RoleName };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (IdentityError identityError in result.Errors)
            {
                ModelState.AddModelError(string.Empty, identityError.Description);
            }

            return View(addRoleViewModel);

        }

        public async  Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role==null)
            {
                return RedirectToAction("Index");
            }

            var editRoleViewModel = new EditRoleViewModel()
            {
                Id = id,
                RoleName = role.Name,
                Users = new List<ApplicationUser>()
            };

            var users = await _userManager.Users.ToListAsync();
            foreach (var u in users)
            {
                if (await _userManager.IsInRoleAsync(u,role.Name))
                {
                    editRoleViewModel.Users.Add(u);
                }
            }

            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(editRoleViewModel.Id);
            if (role!=null)
            {
                role.Name = editRoleViewModel.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "更新角色时出错！");
                    return View(editRoleViewModel);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role!=null)
            {
                var reult = await _roleManager.DeleteAsync(role);
                if (reult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty,"删除角色时出错！");
            }
            ModelState.AddModelError(String.Empty, "未找到角色信息！");
            return RedirectToAction("Index", await _roleManager.Roles.ToListAsync());
        }

        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await  _roleManager.FindByIdAsync(roleId);
            if (role==null)
            {
                return RedirectToAction("Index");
            }

            var vm = new UserRoleViewModel()
            {
                RoleId = role.Id
            };

            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (!await _userManager.IsInRoleAsync(user,role.Name))
                {
                    vm.Users.Add(user);
                }
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            if (user!=null&&role!=null)
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("EditRole",new {id = role.Id});
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
                return View(userRoleViewModel);
            }

            ModelState.AddModelError(string.Empty,"用户或角色未找到！");
            return View(userRoleViewModel);
        }

        public async Task<IActionResult> RemoveUserFromRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            var vm = new UserRoleViewModel()
            {
                RoleId = role.Id
            };

            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Users.Add(user);
                }
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            if (user != null && role != null)
            {
                if (await _userManager.IsInRoleAsync(user,role.Name))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("EditRole", new { id = role.Id });
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(userRoleViewModel);
                }
                ModelState.AddModelError(String.Empty, "用户不属于该角色！");
                return View(userRoleViewModel);
            }

            ModelState.AddModelError(string.Empty, "用户或角色未找到！");
            return View(userRoleViewModel);
        }

        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> CheckRoleExist([Bind("RoleName")] string roleName)
        {
            var role = await  _roleManager.FindByNameAsync(roleName);
            if (role!=null)
            {
                return Json(false);
            }

            return Json(true);
        }
    }
}