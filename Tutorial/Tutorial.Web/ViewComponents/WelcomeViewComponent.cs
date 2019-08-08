using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tutorial.Web.IServices;
using Tutorial.Web.Models;

namespace Tutorial.Web.ViewComponents
{
    public class WelcomeViewComponent:ViewComponent
    {
        private readonly IRepository<Student> _repository;

        public WelcomeViewComponent(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var count = _repository.GetAll().Count().ToString();
            return View("Default", count);
        }
    }
}

