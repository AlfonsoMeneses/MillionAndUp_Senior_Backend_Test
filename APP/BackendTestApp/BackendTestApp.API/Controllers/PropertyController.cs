using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Exceptions;
using BackendTestApp.Contracts.Models;
using BackendTestApp.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        public readonly IPropertyService _service;

        public PropertyController(IPropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PropertyFilter filter )
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

        [HttpPost]
        public IActionResult Create([FromBody] CreatePropertyRequest newProperty)
        {
            try
            {
                var res =  _service.Create(newProperty.Name, newProperty.Address, newProperty.Price, newProperty.Year);
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
                var res = _service.ChangePrice(id,req.Price);
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
    }
}
