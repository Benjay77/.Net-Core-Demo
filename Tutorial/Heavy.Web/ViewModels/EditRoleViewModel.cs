using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Heavy.Web.Models;

namespace Heavy.Web.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required, Display(Name = "角色名")]
        public string RoleName { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}