using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Service.Contracts
{
    public interface ICacheService<T>
    {
        public Task<T> Execute(int id, Func<int, Task<T>> valueGetter);
        public Task<List<T>> Execute(Func<Task<List<T>>> valueGetter);
        public bool IsValueExists(int id);
        public T GetValue(int id);
        public void SetValue(int id, T value);
        public bool IsListExists();
        public List<T> GetList();
        public  void SetList(List<T> list);
        public void DeleteCachedView(int id);
        public void DeleteCachedView();

    }
}
