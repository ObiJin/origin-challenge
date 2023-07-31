using System.Linq.Expressions;

namespace ATM.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? Get(int id);

        List<T> Find(Expression<Func<T, bool>> searchParams);

        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
