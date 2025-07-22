using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroProject.Domain.Interface
{
    public interface IRepository<T>
    {
        public T Create(T entity);
        public List<T> Get();
        public T Update(T entity);
        public bool Delete(int id);

    }
}
