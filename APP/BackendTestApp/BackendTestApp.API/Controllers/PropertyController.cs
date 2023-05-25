using AutoMapper;
using BackendTestApp.API.Helpers;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Exceptions;
using BackendTestApp.Contracts.Models;
using BackendTestApp.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestApp.API.Controllers
{
    /// <summary>
    /// Property Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        /// <summary>
        /// Property Service
        /// </summary>
        public readonly IPropertyService _service;

        /// <summary>
        /// Mapper Service
        /// </summary>
        private readonly IMapper _mapper;

        public PropertyController(IPropertyService service, IMapper mapper)
        {
            //Setting services by dependency injection
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the properties by filters
        /// </summary>
        /// <param name="filter">Properties filters</param>
        /// <returns>List Of Properties</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] PropertyFilter filter)
        {
            try
            {
                //Getting the properties using the property service
                var res = _service.GetProperties(filter);

                //Send data
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                //Setting the error data
                var res = new
                {
                    Error = pex.Message
                };

                //Send error
                return BadRequest(res);
            }
            catch (Exception)
            {
                //Setting the unhandled error data
                var res = new
                {
                    Error = "Internal error, try later"
                };

                //Send error 
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Getting a property by ID
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <returns>Property found</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                //Getting the property by ID using the property service
                var res = _service.GetPropertyById(id);

                //Send data
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                //Setting the error data
                var res = new
                {
                    Error = pex.Message
                };

                //Send error
                return BadRequest(res);
            }
            catch (Exception)
            {
                //Setting the unhandled error data
                var res = new
                {
                    Error = "Internal error, try later"
                };

                //Send error
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Create a new property
        /// </summary>
        /// <param name="newProperty">Property data</param>
        /// <returns>The created property</returns>
        [HttpPost]
        public IActionResult Create([FromBody] CreatePropertyRequest newProperty)
        {
            try
            {
                //Create the property using the property service
                var res = _service.Create(newProperty.Name, newProperty.Address, newProperty.Price, newProperty.Year, newProperty.IdOwner);

                //Send data
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                //Setting the error data
                var res = new
                {
                    Error = pex.Message
                };

                //Send error
                return BadRequest(res);
            }
            catch (Exception)
            {
                //Setting the unhandled error data
                var res = new
                {
                    Error = "Internal error, try later"
                };

                //Send error
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Update a property
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <param name="req">Property data to update</param>
        /// <returns>Property updated</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EditPropertyRequest req)
        {
            try
            {
                //Mapping to set the update data 
                var property = _mapper.Map<PropertyDto>(req);

                //Update the property using the property service
                var res = _service.Update(id, property);

                //Send data
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                //Setting the error data
                var res = new
                {
                    Error = pex.Message
                };

                //Send error
                return BadRequest(res);
            }
            catch (Exception)
            {
                //Setting the unhandled error data
                var res = new
                {
                    Error = "Internal error, try later"
                };

                //Send error
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Change the property price
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <param name="req">Price to change</param>
        /// <returns>Property updated</returns>
        [HttpPatch("{id}")]
        public IActionResult ChangePrice(int id, [FromBody] EditPropertyRequest req)
        {
            try
            {
                //Change the property price using the property service
                var res = _service.ChangePrice(id, req.Price);

                //Send data
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                //Setting the error data
                var res = new
                {
                    Error = pex.Message
                };

                //Send error
                return BadRequest(res);
            }
            catch (Exception)
            {
                //Setting the unhandled error data
                var res = new
                {
                    Error = "Internal error, try later"
                };

                //Send error
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Get a property image
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <param name="idPropertyImage">Property Image ID</param>
        /// <returns>Property Image</returns>
        [HttpGet("{id}/image/{idPropertyImage}")]
        public IActionResult GetImage(int id, int idPropertyImage)
        {
            try
            {
                //Getting the property image using the property service
                var res = _service.GetImage(id, idPropertyImage);

                //Send image
                return File(res, "image/jpeg");
            }
            catch (PropertyException pex)
            {
                //Setting the error data
                var res = new
                {
                    Error = pex.Message
                };

                //Send error
                return BadRequest(res);
            }
            catch (Exception)
            {
                //Setting the unhandled error data
                var res = new
                {
                    Error = "Internal error, try later"
                };

                //Send error
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Add an image to a property
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <returns>Property Image Data</returns>
        [HttpPost("{id}/image")]
        public IActionResult AddImage(int id)
        {
            try
            {
                //Validate th Content Type
                if (Request.ContentType == null || !Request.ContentType.Contains("multipart/form-data"))
                {
                    throw new PropertyException("Invalid Content Type");
                }

                //Validate if an image was sent
                if (Request.Form.Files.Count == 0)
                {
                    throw new PropertyException("Image required");
                }

                //Getting the image 
                var image = Request.Form.Files[0];

                //Validate the image
                if (image == null || image.Length <= 0)
                {
                    throw new PropertyException("Invalid Image");
                }

                //Getting the image bytes
                var file = FileHelper.GetFileBytes(image);

                //Adding the property image using the property service
                var resp = _service.AddImage(id, file);

                //Send property image data
                return Ok(resp);
            }
            catch (PropertyException pex)
            {
                //Setting the error data
                var res = new
                {
                    Error = pex.Message
                };

                //Send error
                return BadRequest(res);
            }
            catch (Exception)
            {
                //Setting the unhandled error data
                var res = new
                {
                    Error = "Internal error, try later"
                };

                //Send error
                return StatusCode(500, res);
            }
        }
    }
}
