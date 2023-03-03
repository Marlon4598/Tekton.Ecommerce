using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Ecommerce.Infrastructure.Interface
{
    public interface IGeneralRepository<T> where T : class
    {
        #region Metodos Sincronos
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string Id);
        T Get(string Id);
        IEnumerable<T> GetAll();
        #endregion

        #region Metodos Asyncronos
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string Id);
        Task<T> GetAsync(string Id);
        Task<IEnumerable<T>> GetAllAsync();
        #endregion
    }
}

