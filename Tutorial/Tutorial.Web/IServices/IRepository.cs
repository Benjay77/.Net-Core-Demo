using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Web.Models;

namespace Tutorial.Web.IServices
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetDetailById(int id);
        T Add(T newModel);
    }
}
