using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FFSAPI.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAll();                                                
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetById(int id);
        Task Create(T entity);
        void Update(T entity);
        Task Delete(int id);

        Task Save();
    }
}
