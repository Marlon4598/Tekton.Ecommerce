using Tekton.Ecommerce.Domain.Entity;
using Tekton.Ecommerce.Domain.Interface;
using Tekton.Ecommerce.Infrastructure.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Tekton.Ecommerce.Domain.Core
{
    public class ProductsDomain : IProductsDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Métodos Síncronos

        public bool Insert(Products products)
        {
            return _unitOfWork.Product.Insert(products);
        }

        public bool Update(Products products)
        {
            return _unitOfWork.Product.Update(products);
        }

        public bool Delete(string productId)
        {
            var product = _unitOfWork.Product.Get(productId);
            if (product != null)
            {
                return _unitOfWork.Product.Delete(productId);
            }
                else
            {
                return false;
            }
        }

        public Products Get(string productId)
        {
            return _unitOfWork.Product.Get(productId);
        }

        public IEnumerable<Products> GetAll()
        {
            return _unitOfWork.Product.GetAll();
        }

        #endregion

        #region Métodos Asíncronos

        public async Task<bool> InsertAsync(Products products)
        {
            return await _unitOfWork.Product.InsertAsync(products);
        }

        public async Task<bool> UpdateAsync(Products products)
        {
            return await _unitOfWork.Product.UpdateAsync(products);
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            var product = _unitOfWork.Product.GetAsync(Id);
            if (product != null)
            {
                return await _unitOfWork.Product.DeleteAsync(Id);
            }
            else
            {
                return false;
            }
        }

        public async Task<Products> GetAsync(string Id)
        {
            return await _unitOfWork.Product.GetAsync(Id);
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            return await _unitOfWork.Product.GetAllAsync();
        }

        #endregion

    }
}