using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tekton.Ecommerce.Application.DTO;
using Tekton.Ecommerce.Application.Interface;
using Tekton.Ecommerce.Application.Main;

namespace Tekton.Ecommerce.Services.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsApplication _productsApplication;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductsApplication productsApplication, ILogger<ProductController> logger)
        {
            _productsApplication = productsApplication;
            _logger = logger;
        }

        #region "Metodo Sincronos"
        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] ProductsDto productsDto)
        {
            if (productsDto == null)
            {
                return BadRequest();
            }

            var response = _productsApplication.Insert(productsDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] ProductsDto productsDto)
        {
            if (productsDto == null)
                return BadRequest();

            var response = _productsApplication.Update(productsDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(string Productid)
        {
            if (string.IsNullOrEmpty(Productid))
                return BadRequest();

            var response = _productsApplication.Delete(Productid);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get(string Productid)
        {
            if (string.IsNullOrEmpty(Productid))
                return BadRequest();

            var response = _productsApplication.Get(Productid);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var response = _productsApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
        #endregion


        #region "Metodo Asyncronos"
        [HttpPost]
        [Route("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] ProductsDto productsDto)
        {
            if (productsDto == null)
            {
                return BadRequest();
            }

            var response = await _productsApplication.InsertAsync(productsDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductsDto productsDto)
        {
            if (productsDto == null)
                return BadRequest();

            var response = await _productsApplication.UpdateAsync(productsDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete]
        [Route("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(string Productid)
        {
            if (string.IsNullOrEmpty(Productid))
                return BadRequest();

            var response = await _productsApplication.DeleteAsync(Productid);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet]
        [Route("GetAsync")]
        public async Task<IActionResult> GetAsync(string Productid)
        {
            if (string.IsNullOrEmpty(Productid))
                return BadRequest();

            var response = await _productsApplication.GetAsync(Productid);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _productsApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
        #endregion

    }
}
