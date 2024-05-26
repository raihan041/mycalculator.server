using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Data.Repositories
{
    public interface IRepository <T> where T : class
    {
        public T Create(T _object);
        public void Delete(T _object);
        public void Update(T _object);
        public IEnumerable<T> GetAll();
        public T GetById(string Id);
    }
}
