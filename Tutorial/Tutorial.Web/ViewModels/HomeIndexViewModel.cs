using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.Web.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<StudentViewModel> studentViewModels { get; set; }
    }
}
