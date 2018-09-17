using System.Collections.Generic;

namespace CompanyNetCore.Interfaces
{
    public interface IRepository<T>
    {
        List<T> Read();
        T Read(int Id);
        T Create(T elm);
        T Update(T elm);
        T Delete(int Id);
    }
}
