using BackendTestApp.API.Controllers;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestApp.UnitTest
{
    public class ApiChangePropertyPriceTest
    {
        private readonly PropertyController _controller;

        public ApiChangePropertyPriceTest()
        {
            _controller = Config.UnitTestConfig.GetPropertyController();
        }

        [Test]
        public void ChangePriceOk()
        {
            var property = new EditPropertyRequest
            {
                Price = 15000000
            };

            var idProperty = 9;

            var response = (OkObjectResult)_controller.ChangePrice(idProperty, property);

            Assert.IsAssignableFrom<PropertyDto>(response.Value);
        }

        [Test]
        public void ChangePriceWithWrongPrice()
        {
            var property = new EditPropertyRequest
            {
                Price = -1
            };

            var idProperty = 9;

            var response = _controller.ChangePrice(idProperty, property);
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        [Test]
        public void ChangePriceWithWrongId()
        {
            var property = new EditPropertyRequest
            {
                Price = 1500000
            };

            var idProperty = 0;

            var response = _controller.ChangePrice(idProperty, property);
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        [Test]
        public void ChangePriceWithoutData()
        {
            var property = new EditPropertyRequest();

            var idProperty = 0;

            var response = _controller.ChangePrice(idProperty, property);
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }
    }
}
