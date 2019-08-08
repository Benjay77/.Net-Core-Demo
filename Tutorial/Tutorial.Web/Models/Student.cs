using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.Web.Models
{
    public class Student
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public int Id { get; set; }

        /// <summary>
        /// 人名第一个
        /// </summary>
        [Display(Name = "名")]
        public string FirstName { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        [Display(Name = "姓")]
        public string LastName { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "出生日期")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public  Gender  Gender { get; set; }
    }
}
