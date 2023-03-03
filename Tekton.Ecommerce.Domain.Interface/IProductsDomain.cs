using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Ecommerce.Domain.Entity;


namespace Tekton.Ecommerce.Domain.Interface
{
    public interface IProductsDomain
    {
        #region Metodos Sincronos
        bool Insert(Products products);
        bool Update(Products products);
        bool Delete(string productId);
        Products Get(string productId);
        IEnumerable<Products> GetAll();
        #endregion

        #region Metodos Asyncronos
        Task<bool> InsertAsync(Products products);
        Task<bool> UpdateAsync(Products products);
        Task<bool> DeleteAsync(string productId);
        Task<Products> GetAsync(string productId);
        Task<IEnumerable<Products>> GetAllAsync();
        #endregion
    }
}
