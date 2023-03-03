using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekton.Ecommerce.Application.DTO;
using Tekton.Ecommerce.Transversal.Common;
using System.Threading.Tasks;

namespace Tekton.Ecommerce.Application.Interface
{
    public interface IProductsApplication
    {
        #region Metodos Sincronos
        Response<bool> Insert(ProductsDto productsDto);
        Response<bool> Update(ProductsDto productsDto);
        Response<bool> Delete(string productId);
        Response<ProductsDto> Get(string productId);
        Response<IEnumerable<ProductsDto>> GetAll();
        #endregion

        #region Metodos Asyncronos
        Task<Response<bool>> InsertAsync(ProductsDto productsDto);
        Task<Response<bool>> UpdateAsync(ProductsDto productsDto);
        Task<Response<bool>> DeleteAsync(string productId);
        Task<Response<ProductsDto>> GetAsync(string productId);
        Task<Response<IEnumerable<ProductsDto>>> GetAllAsync();
        #endregion
    }
}
