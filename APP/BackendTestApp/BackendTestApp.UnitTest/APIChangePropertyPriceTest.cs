using BackendTestApp.API.Controllers;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestApp.UnitTest
{
    /// <summary>
    /// Change Property Price Test
    /// </summary>
    public class ApiChangePropertyPriceTest
    {
        //API Controller
        private readonly PropertyController _controller;

        //Property ID To Test
        private readonly int _idProperty = 9;

        public ApiChangePropertyPriceTest()
        {
            //Setting Controller
            _controller = Config.UnitTestConfig.GetPropertyController();
        }

        /// <summary>
        /// Change the price of a property successful
        /// </summary>
        [Test]
        public void ChangePriceOk()
        {
            //Set new price
            var property = new EditPropertyRequest
            {
                Price = 15000000
            };

            //Set Property ID
            var idProperty = _idProperty;

            //Use method to change the price
            var response = (OkObjectResult)_controller.ChangePrice(idProperty, property);

            //Validate response
            Assert.IsAssignableFrom<PropertyDto>(response.Value);
        }

        /// <summary>
        /// Change the price of a property with a not valid price
        /// </summary>
        [Test]
        public void ChangePriceWithWrongPrice()
        {
            //Set invalid price
            var property = new EditPropertyRequest
            {
                Price = -1
            };

            //Set Property ID
            var idProperty = _idProperty;

            //Use method to change the price
            var response = _controller.ChangePrice(idProperty, property);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        /// <summary>
        /// Change the price of a property with a not valid property ID
        /// </summary>
        [Test]
        public void ChangePriceWithWrongId()
        {
            //Set new price
            var property = new EditPropertyRequest
            {
                Price = 1500000
            };

            //Set Invalid Property ID
            var idProperty = 0;

            //Use method to change the price
            var response = _controller.ChangePrice(idProperty, property);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        /// <summary>
        /// Change the price of a property without data
        /// </summary>
        [Test]
        public void ChangePriceWithoutData()
        {
            //Set request without data
            var property = new EditPropertyRequest();

            //Set Property ID
            var idProperty = _idProperty;

            //Use method to change the price
            var response = _controller.ChangePrice(idProperty, property);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }
    }
}
