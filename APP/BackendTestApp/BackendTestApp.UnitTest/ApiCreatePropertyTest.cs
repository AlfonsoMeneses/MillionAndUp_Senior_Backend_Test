using BackendTestApp.API.Controllers;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BackendTestApp.UnitTest
{
    /// <summary>
    /// Create A Property Test
    /// </summary>
    public class ApiCreatePropertyTest
    {
        //API Controller
        private readonly PropertyController _controller;

        public ApiCreatePropertyTest()
        {
            //Setting Controller
            _controller = Config.UnitTestConfig.GetPropertyController();
        }


        /// <summary>
        /// Create a property successful
        /// </summary>
        [Test]
        public void CreatePropertyOk()
        {
            //Set new property
            var property = new CreatePropertyRequest
            {
                IdOwner = 2,
                Name = "Create Property Ok Testing - " + DateTime.Now.Ticks,
                Address = "Cra 34 #23-65a",
                Price = 2400100,
                Year = 2019
            };

            //Use method to create the property
            var response = (OkObjectResult)_controller.Create(property);

            //Validate response
            Assert.IsAssignableFrom<PropertyDto>(response.Value);
        }

        /// <summary>
        /// Create a property without data
        /// </summary>
        [Test]
        public void CreatePropertyNotOk()
        {
            //Set property without data
            var property = new CreatePropertyRequest();

            //Use method to create the property
            var response = _controller.Create(property);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        /// <summary>
        /// Create a property with a wrong owner ID
        /// </summary>
        [Test]
        public void CreatePropertyWithWrongOwner()
        {
            // Set property with a wrong owner ID
            var property = new CreatePropertyRequest
            {
                IdOwner = -1,
                Name = "Testing",
                Address = "Cra 34 #23-65a",
                Price = 2400100,
                Year = 2019
            };

            //Use method to create the property
            var response = _controller.Create(property);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        /// <summary>
        /// Create a property with a wrong year
        /// </summary>
        [Test]
        public void CreatePropertyWithWrongYear()
        {
            // Set property with a wrong year
            var property = new CreatePropertyRequest
            {
                IdOwner = 2,
                Name = "Testing",
                Address = "Cra 34 #23-65a",
                Price = 2400100,
                Year = 15
            };

            //Use method to create the property
            var response = _controller.Create(property);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        /// <summary>
        /// Create a property with a wrong price
        /// </summary>
        [Test]
        public void CreatePropertyWithWrongPrice()
        {
            // Set property with a wrong price
            var property = new CreatePropertyRequest
            {
                IdOwner = 2,
                Name = "New Property",
                Address = "Cra 34 #23-65a",
                Price = -1,
                Year = 2019
            };

            //Use method to create the property
            var response = _controller.Create(property);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        /// <summary>
        /// Create a property with a duplicate name
        /// </summary>
        [Test]
        public void CreatePropertyWithADuplicateName()
        {
            // Set a property data
            var property = new CreatePropertyRequest
            {
                IdOwner = 2,
                Name = "Duplicate Name - " + DateTime.Now.Ticks,
                Address = "Cra 34 #23-65a",
                Price = 2400100,
                Year = 2019
            };

            //Use method to create the property
            _controller.Create(property);

            //Use method to create a duplicate property
            var response = _controller.Create(property);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }
    }
}
