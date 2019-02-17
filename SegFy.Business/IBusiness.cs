using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegFy.Business
{
    public interface IBusiness<T> where T : class, new()
    {
        IEnumerable<T> GetAll(T obj);
        T Get(int id);
        void Insert(T obj);
        void Update(T obj);
        bool Delete(int id);
    }
}
