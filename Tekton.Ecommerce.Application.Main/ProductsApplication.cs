using System;
using AutoMapper;
using Tekton.Ecommerce.Application.DTO;
using Tekton.Ecommerce.Application.Interface;
using Tekton.Ecommerce.Domain.Entity;
using Tekton.Ecommerce.Domain.Interface;
using Tekton.Ecommerce.Transversal.Common;
using Tekton.Ecommerce.Application.Validator;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Tekton.Ecommerce.Application.Main
{
    public class ProductsApplication : IProductsApplication
    {
        private readonly IProductsDomain _productsDomain;
        private readonly IMapper _mapper;
        private readonly IDiscountProductDomain _discountProductDomain;
        private readonly IMemoryCache _memoryCache;
        private readonly IAppLogger<ProductsApplication> _logger;
        private readonly ProductDtoValidator _productDtoValidator;


        public ProductsApplication(IProductsDomain productsDomain, IMapper mapper, IDiscountProductDomain discountProductDomain, IAppLogger<ProductsApplication> logger, ProductDtoValidator productDtoValidator, IMemoryCache memoryCache)
        {
            _productsDomain = productsDomain;
            _mapper = mapper;
            _discountProductDomain = discountProductDomain;
            _logger = logger;
            _memoryCache = memoryCache;
            _productDtoValidator = productDtoValidator;
        }

        #region Métodos Síncronos
        public Response<bool> Insert(ProductsDto productsDto)
        {
            var begin = DateTime.Now;
            var response = new Response<bool>();
            try
            {
                //var validation = _productDtoValidator.Validate(new ProductsDto() { Name = productsDto.Name, Stock = productsDto.Stock, Description = productsDto.Description, Price = productsDto.Price });
                //if (!validation.IsValid)
                //{
                //    response.Message = "Errores de Validación";
                //    response.Errors = validation.Errors;
                //    return response;
                //}

                var products = _mapper.Map<Products>(productsDto);
                response.Data = _productsDomain.Insert(products);
                var end = DateTime.Now;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";
                    var log = new Log("Insert Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("Insert Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }

        public Response<bool> Update(ProductsDto productsDto)
        {
            var begin = DateTime.Now;
            var response = new Response<bool>();
            try
            {
                //var validation = _productDtoValidator.Validate(new ProductsDto() { Name = productsDto.Name, Stock = productsDto.Stock, Description = productsDto.Description, Price = productsDto.Price });
                //if (!validation.IsValid)
                //{
                //    response.Message = "Errores de Validación";
                //    response.Errors = validation.Errors;
                //    return response;
                //}

                var products = _mapper.Map<Products>(productsDto);
                response.Data = _productsDomain.Update(products);
                var end = DateTime.Now;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitosa!!!";
                    var log = new Log("Update Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("Update Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }

        public Response<bool> Delete(string productId)
        {
            var begin = DateTime.Now;
            var response = new Response<bool>();
            try
            {
                response.Data = _productsDomain.Delete(productId);
                var end = DateTime.Now;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                    var log = new Log("Delete Product", begin, end, "Ok");
                    log.PrintLog();
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "ID no Existe!!!";
                    var log = new Log("Delete Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("Delete Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }

        public Response<ProductsDto> Get(string productId)
        {
            var begin = DateTime.Now;
            var response = new Response<ProductsDto>();
            try
            {
                var products = _productsDomain.Get(productId);
                response.Data = _mapper.Map<ProductsDto>(products);
                var _cache = new Cache(_memoryCache);
                _cache.SetCache();
                var discount = _discountProductDomain.GetDiscount(products.ProductID);
                discount.Wait();

                if (discount.IsCompleted)
                {
                    var discountVal = Convert.ToByte(discount.Result);
                    response.Data.StatusName = _memoryCache.Get(products.Status) as string;
                    response.Data.Discount = discountVal;
                    response.Data.FinalPrice = products.Price * (100 - discountVal) / 100;
                }
                
                var end = DateTime.Now;
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                    var log = new Log("Get Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("Get Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }

        public Response<IEnumerable<ProductsDto>> GetAll()
        {
            var begin = DateTime.Now;
            var response = new Response<IEnumerable<ProductsDto>>();
            try
            {
                var products = _productsDomain.GetAll();
                response.Data = _mapper.Map<IEnumerable<ProductsDto>>(products);
                var end = DateTime.Now;
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                    var log = new Log("GetAll Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("GetAll Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }
        #endregion


        #region Métodos Asíncronos
        public async Task<Response<bool>> InsertAsync(ProductsDto productsDto)
        {
            var begin = DateTime.Now;
            var response = new Response<bool>();
            try
            {
                var validation = _productDtoValidator.Validate(new ProductsDto() { Name = productsDto.Name, Stock = productsDto.Stock, Description = productsDto.Description, Price = productsDto.Price });
                if (!validation.IsValid)
                {
                    response.Message = "Errores de Validación";
                    response.Errors = validation.Errors;
                    return response;
                }
                var products = _mapper.Map<Products>(productsDto);
                response.Data = await _productsDomain.InsertAsync(products);
                var end = DateTime.Now;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";
                    var log = new Log("InsertAsync Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("InsertAsync Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }

        public async Task<Response<bool>> UpdateAsync(ProductsDto productsDto)
        {
            var begin = DateTime.Now;
            var response = new Response<bool>();
            try
            {
                var validation = _productDtoValidator.Validate(new ProductsDto() { Name = productsDto.Name, Stock = productsDto.Stock, Description = productsDto.Description, Price = productsDto.Price });
                if (!validation.IsValid)
                {
                    response.Message = "Errores de Validación";
                    response.Errors = validation.Errors;
                    return response;
                }
                var products = _mapper.Map<Products>(productsDto);
                response.Data = await _productsDomain.UpdateAsync(products);
                var end = DateTime.Now;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitosa!!!";
                    var log = new Log("UpdateAsync Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("UpdateAsync Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string productId)
        {
            var begin = DateTime.Now;
            var response = new Response<bool>();
            try
            {
                response.Data = await _productsDomain.DeleteAsync(productId);
                var end = DateTime.Now;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                    var log = new Log("DeleteAsync Product", begin, end, "Ok");
                    log.PrintLog();
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "ID no Existe!!!";
                    var log = new Log("DeleteAsync Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("DeleteAsync Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }

        public async Task<Response<ProductsDto>> GetAsync(string productId)
        {
            var begin = DateTime.Now;
            var response = new Response<ProductsDto>();
            try
            {
                var products = await _productsDomain.GetAsync(productId);
                response.Data = _mapper.Map<ProductsDto>(products);
                var _cache = new Cache(_memoryCache);
                _cache.SetCache();

                var discount = _discountProductDomain.GetDiscount(products.ProductID);
                discount.Wait();

                if (discount.IsCompleted)
                {
                    var discountVal = Convert.ToByte(discount.Result);
                    response.Data.StatusName = _memoryCache.Get(products.Status) as string;
                    response.Data.Discount = discountVal;
                    response.Data.FinalPrice = products.Price * (100 - discountVal) / 100;
                }

                var end = DateTime.Now;
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                    var log = new Log("GetAsync Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("GetAsync Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }

        public async Task<Response<IEnumerable<ProductsDto>>> GetAllAsync()
        {
            var begin = DateTime.Now;
            var response = new Response<IEnumerable<ProductsDto>>();
            try
            {
                var products = await _productsDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<ProductsDto>>(products);
                var end = DateTime.Now;
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                    var log = new Log("GetAllAsync Product", begin, end, "Ok");
                    log.PrintLog();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                var end = DateTime.Now;
                var log = new Log("GetAllAsync Product", begin, end, "Error: " + e.Message);
                log.PrintLog();
            }
            return response;
        }
        #endregion

    }
}