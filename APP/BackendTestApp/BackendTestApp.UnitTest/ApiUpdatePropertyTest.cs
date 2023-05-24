using Azure;
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
    public class ApiUpdatePropertyTest
    {
        private readonly PropertyController _controller;

        public ApiUpdatePropertyTest()
        {
            _controller = Config.UnitTestConfig.GetPropertyController();
        }

        [Test]
        public void UpdatePropertyOk()
        {
            var getResponse = (OkObjectResult)_controller.Get(new PropertyFilter());

            Assert.IsAssignableFrom<List<PropertyDto>>(getResponse.Value);

            Assert.IsNotNull(getResponse.Value);

            var lstProperties = (List<PropertyDto>)getResponse.Value;

            Assert.IsNotNull(lstProperties);

            var editProperty = new EditPropertyRequest
            {
                Name = "Name Updated",
                Address = "New Address",
                Price = 15590,
                Year = 2015,
            };

            var idProperty = 1;

            if (lstProperties.Count() > 0)
            {
                idProperty = lstProperties[0].IdProperty;

                var editResponse = (OkObjectResult)_controller.Update(idProperty, editProperty);

                Assert.IsAssignableFrom<PropertyDto>(editResponse.Value);

                Assert.IsNotNull(editResponse.Value);
            }
            else
            {
                var editResponse = _controller.Update(idProperty, editProperty);

                Assert.IsAssignableFrom<BadRequestObjectResult>(editResponse);

                Assert.IsTrue(((BadRequestObjectResult)editResponse).StatusCode == 400);
            }


        }

        [Test]
        public void UpdateANonExistentProperty()
        {
            var editProperty = new EditPropertyRequest
            {
                Name = "Name Updated",
                Address = "New Address",
                Price = 15590,
                Year = 2015,
            };

            var idProperty = -1;
            var editResponse = _controller.Update(idProperty, editProperty);

            Assert.IsAssignableFrom<BadRequestObjectResult>(editResponse);

            Assert.IsTrue(((BadRequestObjectResult)editResponse).StatusCode == 400);
        }

        [Test]
        public void UpdatePropertyWithAYearError()
        {
            var editProperty = new EditPropertyRequest
            {
                Name = "Name Updated",
                Address = "New Address",
                Price = 15590,
                Year = 20,
            };

            var idProperty = -1;
            var editResponse = _controller.Update(idProperty, editProperty);

            Assert.IsAssignableFrom<BadRequestObjectResult>(editResponse);

            Assert.IsTrue(((BadRequestObjectResult)editResponse).StatusCode == 400);
        }
    }
}
