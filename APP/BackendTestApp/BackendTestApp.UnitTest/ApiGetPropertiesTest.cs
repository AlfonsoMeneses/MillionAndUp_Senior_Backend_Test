using BackendTestApp.API.Controllers;
using BackendTestApp.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestApp.UnitTest
{
    /// <summary>
    ///  Get Properties Test
    /// </summary>
    public class ApiGetPropertiesTest
    {
        //API Controller
        private readonly PropertyController _controller;

        //Property ID To Test
        private readonly int _idProperty = 9;

        public ApiGetPropertiesTest()
        {
            //Setting Controller
            _controller = Config.UnitTestConfig.GetPropertyController();
        }

        /// <summary>
        /// Get Properties Successful
        /// </summary>
        [Test]
        public void GetPropertiesTest()
        {
            //Use method to get the properties
            var response = (OkObjectResult)_controller.Get(new PropertyFilter());

            //Validate response
            Assert.IsAssignableFrom<List<PropertyDto>>(response.Value);
        }

        /// <summary>
        /// Get Properties With Invalid Data 
        /// </summary>
        [Test]
        public void GetPropertiesWithInvalidDataTest()
        {
            //Set invalid data in the filters
            var filters = new PropertyFilter
            {
                IdOwner = -1
            };

            //Use method to get the properties
            var response = (OkObjectResult)_controller.Get(filters);

            //Validate Object Type
            Assert.IsAssignableFrom<List<PropertyDto>>(response.Value);

            //Set response value
            var lstResponse = response.Value;

            //Validate value
            Assert.IsNotNull(lstResponse);

            //Validate no data
            Assert.IsTrue(((List<PropertyDto>)lstResponse).Count == 0);
        }

        /// <summary>
        /// Get Property By ID Success
        /// </summary>
        [Test]
        public void GetPropertyByIdOk()
        {
            //Set Property ID
            var idProperty = _idProperty;

            //Use method to get the property by ID
            var response = (OkObjectResult)_controller.GetById(idProperty);

            //Validate response
            Assert.IsAssignableFrom<PropertyDto>(response.Value);

            //Validate response value
            Assert.IsNotNull(response.Value);

            //Get Id Property from the response value
            var property = ((PropertyDto)response.Value).IdProperty;

            //Validate Property ID
            Assert.IsTrue(property == idProperty);
        }

        /// <summary>
        /// Get Property By Wrong ID 
        /// </summary>
        [Test]
        public void GetPropertyErrorTest()
        {
            //Set Wrong Property ID
            var idProperty = -1;

            //Use method to get the property by ID
            var response = (BadRequestObjectResult)_controller.GetById(idProperty);

            //Validate Status Code
            Assert.IsTrue(response.StatusCode == 400);
        }
    }
}