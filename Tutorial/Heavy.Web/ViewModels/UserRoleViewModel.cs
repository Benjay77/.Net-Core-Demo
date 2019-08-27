using System.Collections.Generic;
using Heavy.Web.Models;

namespace Heavy.Web.ViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            Users = new List<ApplicationUser>();
        }

        public string RoleId { get; set; }

        public string UserId { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}