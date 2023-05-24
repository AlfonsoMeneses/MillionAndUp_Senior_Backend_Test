using BackendTestApp.API.Controllers;
using BackendTestApp.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestApp.UnitTest
{
    public class ApiGetPropertiesTest
    {
        private readonly PropertyController _controller;

        public ApiGetPropertiesTest()
        {
            _controller = Config.UnitTestConfig.GetPropertyController();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void GetPropertiesTest()
        {
            var response = (OkObjectResult)_controller.Get(new PropertyFilter());

            Assert.IsAssignableFrom<List<PropertyDto>>(response.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void GetPropertiesWithNotDataTest()
        {
            var filters = new PropertyFilter
            {
                IdOwner = -1
            };

            var response = (OkObjectResult)_controller.Get(filters);

            Assert.IsAssignableFrom<List<PropertyDto>>(response.Value);

            var lstResponse = response.Value;

            Assert.IsNotNull(lstResponse);

            Assert.IsTrue(((List<PropertyDto>)lstResponse).Count == 0);
        }

        [Test]
        public void GetPropertyOkTest()
        {
            var idProperty = 9;

            var response = (OkObjectResult)_controller.GetById(idProperty);

            Assert.IsAssignableFrom<PropertyDto>(response.Value);

            Assert.IsNotNull(response.Value);

            var property = ((PropertyDto)response.Value).IdProperty;

            Assert.IsTrue(property == idProperty);
        }

        [Test]
        public void GetPropertyErrorTest()
        {
            var idProperty = -1;

            var response = (BadRequestObjectResult)_controller.GetById(idProperty);

            Assert.IsTrue(response.StatusCode == 400);
        }
    }
}