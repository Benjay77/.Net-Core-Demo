using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Web.IServices;
using Tutorial.Web.Models;

namespace Tutorial.Web.Services
{
    public class Repository : IRepository<Student>
    {
        private readonly List<Student> students;

        public Repository()
        {
            students = new List<Student>()
            {
                new Student()
            {
                Id =1,
                FirstName = "Benjay",
                LastName ="Shaw",
                BirthDate = new DateTime(1989,2,14)
            },
                new Student()
            {
                Id =2,
                FirstName = "Alice",
                LastName ="Young",
                BirthDate = new DateTime(1990,4,13)
            },
                new Student()
            {
                Id =3,
                FirstName = "Jay",
                LastName ="Chow",
                BirthDate = new DateTime(1979,1,18)
            },
            };
        }

        public IEnumerable<Student> GetAll()
        {
            return students;
        }

        public Student GetDetailById(int id)
        {
            Student student = students.FirstOrDefault(a => a.Id == id);
            return student;
        }

        public Student Add(Student newModel)
        {
            var maxId = students.Max(x => x.Id);
            newModel.Id = maxId + 1;
            students.Add(newModel);
            return newModel;
        }
    }
}
