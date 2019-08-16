using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "用户名"), Required, MaxLength(10)]
        public string UserName { get; set; }

        [Display(Name = "密码"), Required, MaxLength(10), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}