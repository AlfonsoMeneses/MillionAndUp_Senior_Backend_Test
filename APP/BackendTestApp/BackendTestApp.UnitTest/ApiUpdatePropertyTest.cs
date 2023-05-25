using BackendTestApp.API.Controllers;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Models;
using Microsoft.AspNetCore.Mvc;


namespace BackendTestApp.UnitTest
{
    /// <summary>
    /// Update Property Test
    /// </summary>
    public class ApiUpdatePropertyTest
    {
        //API Controller
        private readonly PropertyController _controller;

        //Property ID To Test
        private readonly int _idProperty = 9;

        public ApiUpdatePropertyTest()
        {
            //Setting Controller
            _controller = Config.UnitTestConfig.GetPropertyController();
        }

        /// <summary>
        /// Update Property Success
        /// </summary>
        [Test]
        public void UpdatePropertyOk()
        {
            //Getting all properties
            var getResponse = (OkObjectResult)_controller.Get(new PropertyFilter());

            //validate response 
            Assert.IsAssignableFrom<List<PropertyDto>>(getResponse.Value);

            //validate response value
            Assert.IsNotNull(getResponse.Value);

            //Get the list of properties
            var lstProperties = (List<PropertyDto>)getResponse.Value;

            //Validate list
            Assert.IsNotNull(lstProperties);


            //Set data to update the property
            var editProperty = new EditPropertyRequest
            {
                Name = "Name Updated",
                Address = "New Address",
                Price = 15590,
                Year = 2015,
            };

            //Set default property ID
            var idProperty = 1;

            //If there is data in the list
            if (lstProperties.Count() > 0)
            {
                //Set property ID
                idProperty = lstProperties[0].IdProperty;

                //Use method to update the property
                var editResponse = (OkObjectResult)_controller.Update(idProperty, editProperty);

                //Validate the object type response
                Assert.IsAssignableFrom<PropertyDto>(editResponse.Value);

                //Validate response value
                Assert.IsNotNull(editResponse.Value);
            }
            else
            {
                //Use method to update the property with an ID that doesn't exist
                var editResponse = _controller.Update(idProperty, editProperty);

                //Validate Bad Rquest Response
                Assert.IsAssignableFrom<BadRequestObjectResult>(editResponse);

                //Validate Status Code
                Assert.IsTrue(((BadRequestObjectResult)editResponse).StatusCode == 400);
            }


        }

        /// <summary>
        /// Update A Non Existent Property
        /// </summary>
        [Test]
        public void UpdateANonExistentProperty()
        {
            //Set data to update the property
            var editProperty = new EditPropertyRequest
            {
                Name = "Name Updated",
                Address = "New Address",
                Price = 15590,
                Year = 2015,
            };

            //Set a wrong property ID
            var idProperty = -1;

            //Use method to update the property
            var editResponse = _controller.Update(idProperty, editProperty);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(editResponse);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)editResponse).StatusCode == 400);
        }

        /// <summary>
        /// Update Property With A Wrong Year 
        /// </summary>
        [Test]
        public void UpdatePropertyWithAWrongYear()
        {
            //Set data to update the property with a wrong year
            var editProperty = new EditPropertyRequest
            {
                Name = "Name Updated",
                Address = "New Address",
                Price = 15590,
                Year = 20,
            };

            //Set a property ID
            var idProperty = _idProperty;

            //Use method to update the property
            var editResponse = _controller.Update(idProperty, editProperty);

            //Validate Bad Rquest Response
            Assert.IsAssignableFrom<BadRequestObjectResult>(editResponse);

            //Validate Status Code
            Assert.IsTrue(((BadRequestObjectResult)editResponse).StatusCode == 400);
        }
    }
}
