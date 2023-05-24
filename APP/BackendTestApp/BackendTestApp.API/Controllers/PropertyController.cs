using AutoMapper;
using BackendTestApp.API.Helpers;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Exceptions;
using BackendTestApp.Contracts.Models;
using BackendTestApp.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        public readonly IPropertyService _service;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PropertyFilter filter)
        {
            try
            {
                var res = _service.GetProperties(filter);
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                var res = new
                {
                    Error = pex.Message
                };

                return BadRequest(res);
            }
            catch (Exception)
            {
                var res = new
                {
                    Error = "Internal error, try later"
                };

                return StatusCode(500, res);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var res = _service.GetPropertyById(id);
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                var res = new
                {
                    Error = pex.Message
                };

                return BadRequest(res);
            }
            catch (Exception)
            {
                var res = new
                {
                    Error = "Internal error, try later"
                };

                return StatusCode(500, res);
            }
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreatePropertyRequest newProperty)
        {
            try
            {
                var res = _service.Create(newProperty.Name, newProperty.Address, newProperty.Price, newProperty.Year, newProperty.IdOwner);
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                var res = new
                {
                    Error = pex.Message
                };

                return BadRequest(res);
            }
            catch (Exception)
            {
                var res = new
                {
                    Error = "Internal error, try later"
                };

                return StatusCode(500, res);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EditPropertyRequest req)
        {
            try
            {
                var property = _mapper.Map<PropertyDto>(req);
                var res = _service.Update(id, property);
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                var res = new
                {
                    Error = pex.Message
                };

                return BadRequest(res);
            }
            catch (Exception)
            {
                var res = new
                {
                    Error = "Internal error, try later"
                };

                return StatusCode(500, res);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult ChangePrice(int id, [FromBody] EditPropertyRequest req)
        {
            try
            {
                var res = _service.ChangePrice(id, req.Price);
                return Ok(res);
            }
            catch (PropertyException pex)
            {
                var res = new
                {
                    Error = pex.Message
                };

                return BadRequest(res);
            }
            catch (Exception)
            {
                var res = new
                {
                    Error = "Internal error, try later"
                };

                return StatusCode(500, res);
            }
        }

        [HttpGet("{id}/image/{idPropertyImage}")]
        public IActionResult GetImage(int id, int idPropertyImage )
        {
            try
            {
                var res = _service.GetImage(id, idPropertyImage);
                return File(res, "image/jpeg");
            }
            catch (PropertyException pex)
            {
                var res = new
                {
                    Error = pex.Message
                };

                return BadRequest(res);
            }
            catch (Exception)
            {
                var res = new
                {
                    Error = "Internal error, try later"
                };

                return StatusCode(500, res);
            }
        }

        [HttpPost("{id}/image")]
        public IActionResult AddImage(int id)
        {
            try
            {
                if (Request.ContentType == null || !Request.ContentType.Contains("multipart/form-data"))
                {
                    throw new PropertyException("Invalid Content Type");
                }

                if (Request.Form.Files.Count == 0)
                {
                    throw new PropertyException("Image required");
                }

                var image = Request.Form.Files[0];

                if (image == null || image.Length <= 0)
                {
                    throw new PropertyException("Invalid Image");
                }

                var file = FileHelper.GetFileBytes(image);
                var resp = _service.AddImage(id, file);

                return Ok(resp);
            }
            catch (PropertyException pex)
            {
                var res = new
                {
                    Error = pex.Message
                };

                return BadRequest(res);
            }
            catch (Exception)
            {
                var res = new
                {
                    Error = "Internal error, try later"
                };

                return StatusCode(500, res);
            }
        }
    }
}
