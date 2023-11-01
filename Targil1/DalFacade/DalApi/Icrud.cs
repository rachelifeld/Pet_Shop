using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface Icrud <T>
    {
        public int Add(T item);
        public T Get(int id);
        public T Get(Predicate<T> func);
        public void Update(T item);
        public void Delete(int id);
        //public IEnumerable<T> ReadAll();
        public IEnumerable<T> ReadAll(Func<T,bool>? func=null);

    }
}
