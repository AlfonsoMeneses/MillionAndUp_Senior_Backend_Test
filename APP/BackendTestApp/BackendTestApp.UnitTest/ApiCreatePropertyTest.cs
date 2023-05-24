using BackendTestApp.API.Controllers;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.UnitTest
{
    public class ApiCreatePropertyTest
    {
        private readonly PropertyController _controller;

        public ApiCreatePropertyTest()
        {
            _controller = Config.UnitTestConfig.GetPropertyController();
        }

        [Test]
        public void CreatePropertyOk()
        {
            var property = new CreatePropertyRequest
            {
                IdOwner = 2,
                Name = "Create Property Ok Testing - " + DateTime.Now.Ticks,
                Address = "Cra 34 #23-65a",
                Price = 2400100,
                Year = 2019
            };

            var response = (OkObjectResult)_controller.Create(property);
            Assert.IsAssignableFrom<PropertyDto>(response.Value);
        }

        [Test]
        public void CreatePropertyNotOk()
        {
            var property = new CreatePropertyRequest();

            var response = _controller.Create(property);
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        [Test]
        public void CreatePropertyWithWrongOwner()
        {
            var property = new CreatePropertyRequest
            {
                IdOwner = -1,
                Name = "Testing"  ,
                Address = "Cra 34 #23-65a",
                Price = 2400100,
                Year = 2019
            }; 

            var response = _controller.Create(property);
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        [Test]
        public void CreatePropertyWithWrongYear()
        {
            var property = new CreatePropertyRequest
            {
                IdOwner = 2,
                Name = "Testing",
                Address = "Cra 34 #23-65a",
                Price = 2400100,
                Year = 15
            };

            var response = _controller.Create(property);
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        [Test]
        public void CreatePropertyWithWrongPrice()
        {
            var property = new CreatePropertyRequest
            {
                IdOwner = 2,
                Name = "New Property",
                Address = "Cra 34 #23-65a",
                Price = -1,
                Year = 2019
            };

            var response = _controller.Create(property);
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }

        [Test]
        public void CreatePropertyWithADuplicateName()
        {

            var property = new CreatePropertyRequest
            {
                IdOwner = 2,
                Name = "Duplicate Name - "+DateTime.Now.Ticks,
                Address = "Cra 34 #23-65a",
                Price = 2400100,
                Year = 2019
            };

            _controller.Create(property);

            var response =_controller.Create(property);

            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            Assert.IsTrue(((BadRequestObjectResult)response).StatusCode == 400);
        }
    }
}
