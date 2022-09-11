using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NLayer.API.Controllers;
using NLayer.Core;
using NLayer.Core.Dtos;
using NLayer.Core.Services;
using System.Net;
using System.Threading.Tasks;
using Xunit;


namespace ProductTest
{
    public class ProductTest
    {
        [Fact]
        public void Post_Product_Success()
        {
            var productRepository = new Mock<IProductService>();
            var mockMapper = new Mock<IMapper>();
            var productController = new ProductsController(mockMapper.Object,productRepository.Object);
            var actionResultTask = productController.Save(new ProductDto
            {
                Id = 52,
                CategoryId = 1,
                Name = "Test",
                Stock= 3,
                CreateDate = System.DateTime.Now,
                Price = 34
            });
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as IActionResult;
            var resultStatusCode = viewResult.GetType().GetProperty("StatusCode").GetValue(actionResultTask.Result);
            Assert.NotNull(resultStatusCode);
            Assert.Equal((int)HttpStatusCode.Created, resultStatusCode);

        }

        [Fact]
        public void Post_Product_BadRequest()
        {
            var productRepository = new Mock<IProductService>();
            var mockMapper = new Mock<IMapper>();
            var productController = new ProductsController(mockMapper.Object, productRepository.Object);
            var actionResultTask = productController.Save(null);
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as IActionResult;
            var resultStatusCode = viewResult.GetType().GetProperty("StatusCode").GetValue(actionResultTask.Result);
            Assert.NotNull(resultStatusCode);
            Assert.Equal((int)HttpStatusCode.BadRequest, resultStatusCode);
        }
        
        [Fact]
        public void Remove_Product_Bad_Request()
        {
            var productRepository = new Mock<IProductService>();
            var mockMapper = new Mock<IMapper>();
            var productController = new ProductsController(mockMapper.Object, productRepository.Object);
            var actionResultTask = productController.Remove(0);
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as IActionResult;
            var resultStatusCode = viewResult.GetType().GetProperty("StatusCode").GetValue(actionResultTask.Result);
            Assert.NotNull(resultStatusCode);
            Assert.Equal((int)HttpStatusCode.BadRequest, resultStatusCode);
        }

    }
}
