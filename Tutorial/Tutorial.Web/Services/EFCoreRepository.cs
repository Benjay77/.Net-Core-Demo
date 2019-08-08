using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Web.Data;
using Tutorial.Web.IServices;
using Tutorial.Web.Models;

namespace Tutorial.Web.Services
{
    public class EFCoreRepository : IRepository<Student>
    {
        private readonly DataContext _dataContext;

        public EFCoreRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Student Add(Student newModel)
        {
             _dataContext.Students.Add(newModel);
             _dataContext.SaveChanges();
             return newModel;
        }

        public IEnumerable<Student> GetAll()
        {
            return _dataContext.Students.ToList();
        }

        public Student GetDetailById(int id)
        {
            return _dataContext.Students.Find(id);
        }
    }
}
