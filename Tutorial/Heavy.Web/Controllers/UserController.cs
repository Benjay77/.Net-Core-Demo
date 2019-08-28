using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Heavy.Web.Data;
using Heavy.Web.Models;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Controllers
{
    [Authorize("Administrator")]
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

            var  user = new ApplicationUser(){UserName = addUserViewModel.UserName,Email = addUserViewModel.Email,IdCardNo = addUserViewModel.IdCardNo,BirthDate = addUserViewModel.BirthDate};
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

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                var claims = await _userManager.GetClaimsAsync(user);

                var vm = new EditUserViewModel
                {
                    Id = user.Id,
                    BirthDate = user.BirthDate,
                    IdCardNo = user.IdCardNo,
                    UserName =  user.UserName,
                    Email = user.Email,
                    Claims = claims.Select(x=>x.Value).ToList()
                };

                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult>  EditUser(string id,EditUserViewModel editUserViewModel)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = editUserViewModel.UserName;
                user.Email = editUserViewModel.Email;
                user.IdCardNo = editUserViewModel.IdCardNo;
                user.BirthDate = editUserViewModel.BirthDate;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError identityError in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError.Description);
                }

                return View(editUserViewModel);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "未找到用户！");
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult>  ManageClaims(string id)
        {
            var user = await _userManager.Users.Include(x=>x.Claims).Where(x=>x.Id==id).SingleOrDefaultAsync();
            if (user==null)
            {
                return RedirectToAction("Index");
            }

            var leftClaims = ClaimTypes.AllClaimTypeList.Except(user.Claims.Select(x => x.ClaimType)).ToList();

            var vm = new ManageClaimsViewModel
            {
                UserId = user.Id,
                AvailableClaims = leftClaims
            };
            return View(vm);

        }
    }
}