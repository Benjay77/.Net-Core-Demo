using System.ComponentModel.DataAnnotations;

namespace Heavy.Web.ViewModels
{
    public class AddRoleViewModel
    {
        [Required, Display(Name = "角色名")]
        public string RoleName { get; set; }
    }
}