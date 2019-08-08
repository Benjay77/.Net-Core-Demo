using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.Web.Controllers
{
    [Route("v0/[controller]/[action]")]
    public class AboutController
    {
        public string Me()
        {
            return "Benjay";
        }

        public string Company()
        {
            return "No Company";
        }
    }
}
