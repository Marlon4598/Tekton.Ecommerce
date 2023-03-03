using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Tekton.Ecommerce.Application.DTO;
using Tekton.Ecommerce.Application.Main;
using Tekton.Ecommerce.Application.Validator;
using Tekton.Ecommerce.Domain.Core;
using Tekton.Ecommerce.Infrastructure.Data;
using Tekton.Ecommerce.Infrastructure.Repository;
using Tekton.Ecommerce.Services.WebApi.Controllers;
using Tekton.Ecommerce.Transversal.Common;
using Tekton.Ecommerce.Transversal.Logging;

namespace Tekton.Ecommerce.Application.Test
{
    public class ProductTest
    {
        private Mock<ILoggerFactory> _mockLoggerFactory;
        private Mock<IHttpClientFactory> _mockClientFactory;
        private Mock<IConfigurationSection> _mockConfigSection;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<DapperContext> _mockDapper;
        private Mock<ProductsRepository> _mockRepository;
        private Mock<DiscountProductRepository> _mockDiscountRepository;
        private Mock<UnitOfWork> _mockUnit;
        private Mock<ProductsDomain> _mockDomain;
        private IMapper _mapper;
        private Mock<DiscountProductDomain> _mockDiscount;
        private Mock<LoggerAdapter<ProductsApplication>> _mockLogger;
        private Mock<ProductDtoValidator> _mockValidator;
        private Mock<ProductsApplication> _mockApplication;
        private Mock<ILogger<ProductController>> _mockLog;

        [SetUp]
        public void Setup()
        {
            _mockLoggerFactory = new Mock<ILoggerFactory>();

            _mockClientFactory = new Mock<IHttpClientFactory>();

            //var clientHandlerStub = new DelegatingHandlerStub();
            //var client = new HttpClient(clientHandlerStub);

            //_mockClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            _mockConfigSection = new Mock<IConfigurationSection>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfigSection.SetupGet(m => m[It.Is<string>(s => s == "VirtualStoreConnection")]).Returns("server=.; database=VirtualStore; Integrated Security= true;");

            _mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(_mockConfigSection.Object);


            _mockDapper = new Mock<DapperContext>(_mockConfiguration.Object);
            _mockRepository = new Mock<ProductsRepository>(_mockDapper.Object);
            _mockDiscountRepository = new Mock<DiscountProductRepository>(_mockClientFactory.Object);
            _mockUnit = new Mock<UnitOfWork>(_mockRepository.Object);
            _mockDomain = new Mock<ProductsDomain>(_mockUnit.Object);

            var mappingConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingsProfile());
            });
            IMapper mapper = mappingConfiguration.CreateMapper();
            _mapper = mapper;
                        
            _mockDiscount = new Mock<DiscountProductDomain>(_mockDiscountRepository.Object);
            _mockLogger = new Mock<LoggerAdapter<ProductsApplication>>(_mockLoggerFactory.Object);

            object expected=null;
            var _mockMemory = MockMemoryCacheService.GetMemoryCache(expected);
            _mockValidator = new Mock<ProductDtoValidator>();

            
            _mockApplication = new Mock<ProductsApplication>(_mockDomain.Object, _mapper, _mockDiscount.Object, _mockLogger.Object, _mockValidator.Object, _mockMemory);
            _mockLog = new Mock<ILogger<ProductController>>();
        }

        [Test]
        public void Delete_EnvioParametros_MensajeCorrecto()
        {
            var log = _mockLog.Object;
            var app = _mockApplication.Object;
            var x = new ProductController(app, log);

            var id = "15";
            var expected = false;
         
            var contentResult = (OkObjectResult)x.Delete(id);
            var result = (Response<bool>)contentResult.Value;

            Assert.AreEqual(expected, result.Data);
        }

        [Test]
        public void Post_EnvioParametros_MensajeCorrecto()
        {
            var log = _mockLog.Object;
            var app = _mockApplication.Object;
            var x = new ProductController(app, log);

            var producto = new ProductsDto();
            producto.Name = "Video Plus";
            producto.Status = true;
            producto.Stock = 15;
            producto.Description = "Video Plus";
            producto.Price = 20;

            var expected = true;

            var contentResult = (OkObjectResult)x.Insert(producto);
            var result = (Response<bool>)contentResult.Value;

            Assert.AreEqual(expected, result.Data);
        }

        [Test]
        public void Put_EnvioParametros_MensajeCorrecto()
        {
            var log = _mockLog.Object;
            var app = _mockApplication.Object;
            var x = new ProductController(app, log);

            var producto = new ProductsDto();
            producto.ProductID = 22;
            producto.Name = "Video Plus";
            producto.Status = true;
            producto.Stock = 15;
            producto.Description = "Video Plus";
            producto.Price = 20;

            var expected = true;

            var contentResult = (OkObjectResult)x.Update(producto);
            var result = (Response<bool>)contentResult.Value;

            Assert.AreEqual(expected, result.Data);
        }

        [Test]
        public void GetAll_EnvioParametros_MensajeCorrecto()
        {
            var log = _mockLog.Object;
            var app = _mockApplication.Object;
            var x = new ProductController(app, log);

            var expected = 23;

            var contentResult = (OkObjectResult)x.GetAll();
            var result = (Response<IEnumerable<ProductsDto>>)contentResult.Value;

            Assert.AreEqual(expected, result.Data.Count());
        }

        [Test]
        public void Get_EnvioParametros_MensajeCorrecto()
        {
            var log = _mockLog.Object;
            var app = _mockApplication.Object;
            var x = new ProductController(app, log);

            var expected = "CPU";

            var contentResult = (OkObjectResult)x.Get("16");
            var result = (Response<ProductsDto>)contentResult.Value;

            Assert.AreEqual(expected, result.Data.Name);
        }



    }

    public static class MockMemoryCacheService
    {
        public static IMemoryCache GetMemoryCache(object expectedValue)
        {
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedValue))
                .Returns(true);
            return mockMemoryCache.Object;
        }
    }

    //public class DelegatingHandlerStub : DelegatingHandler
    //{
    //    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
    //    public DelegatingHandlerStub()
    //    {
    //        _handlerFunc = (request, cancellationToken) => Task.FromResult(request.CreateResponse(HttpStatusCode.OK));
    //    }

    //    public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
    //    {
    //        _handlerFunc = handlerFunc;
    //    }

    //    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        return _handlerFunc(request, cancellationToken);
    //    }
    //}


}