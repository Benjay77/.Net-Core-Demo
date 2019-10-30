using System.ComponentModel.DataAnnotations;
using Heavy.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Heavy.Web.ViewModels
{
    public class AddRoleViewModel
    {
        [Required, Display(Name = "角色名")]
        [Remote(nameof(RoleController.CheckRoleExist),"Role","角色已存在！")]
        public string RoleName { get; set; }
    }
}