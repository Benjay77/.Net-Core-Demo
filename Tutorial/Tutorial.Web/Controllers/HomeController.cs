using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Tutorial.Web.IServices;
using Tutorial.Web.Models;
using Tutorial.Web.ViewModels;

namespace Tutorial.Web.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly IRepository<Student> _repository;

        public HomeController(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var list = _repository.GetAll();
            var vms = list.Select(x => new StudentViewModel {Id = x.Id, Name = $"{x.FirstName} {x.LastName}" ,Age = DateTime.Now.Subtract(x.BirthDate).Days/365});
            var vm = new HomeIndexViewModel { studentViewModels = vms };
            return View(vm);
            //return Content("Hello from HomeController");
        }

        public IActionResult Detail(int id)
        {
            var student = _repository.GetDetailById(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(student);
            //return Content(id.ToString());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentCreateViewModel student)
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    Gender = student.Gender
                };
                var newModel = _repository.Add(newStudent);
                return RedirectToAction(nameof(Detail), new { id = newModel.Id });
            }
            ModelState.AddModelError(string.Empty,"Model is error!");
            return View();
        }
    }
}
