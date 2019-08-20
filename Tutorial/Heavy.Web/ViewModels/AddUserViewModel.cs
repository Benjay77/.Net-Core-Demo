using System;
using System.ComponentModel.DataAnnotations;

namespace Heavy.Web.ViewModels
{
    public class AddUserViewModel
    {
        [Required,Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required, Display(Name = "邮箱"),DataType(DataType.EmailAddress),RegularExpression(@"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?")]
        public string Email { get; set; }

        [Required, Display(Name = "密码"),DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Display(Name = "身份证号"), MaxLength(18)]
        public string IdCardNo { get; set; }

        [Required, Display(Name = "出生日期"), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}