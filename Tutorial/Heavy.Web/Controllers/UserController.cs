using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Heavy.Web.Models;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await  _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"删除用户时发生错误！");
                }
            }
            else
            {
                    ModelState.AddModelError(string.Empty,"未找到用户！");
            }

            return View("Index", await _userManager.Users.ToListAsync());
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addUserViewModel);
            }

            var  user = new ApplicationUser(){UserName = addUserViewModel.UserName,Email = addUserViewModel.Email};
            var result = await _userManager.CreateAsync(user,addUserViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", await _userManager.Users.ToListAsync());
            }

            foreach (IdentityError identityError in result.Errors)
            {
                ModelState.AddModelError(string.Empty, identityError.Description);
            }

            return View(addUserViewModel);
        }

        [HttpGet]
        public IActionResult EditUser()
        {
            return View("AddUser");
        }

        [HttpPost]
        public async Task<IActionResult>  EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError identityError in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError.Description);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "未找到用户！");
            }

            return View("Index", await _userManager.Users.ToListAsync());
        }
    }
}