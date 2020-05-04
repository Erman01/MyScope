using MyScope.Core.Models;
using System.Linq;

namespace MyShop.Core
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string id);
        T Find(string id);
        void Insert(T TEntity);
        void Update(T TEntity);
    }
}