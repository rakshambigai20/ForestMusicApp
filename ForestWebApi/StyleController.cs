using Microsoft.AspNetCore.Mvc;
using Forest.OutServices.IService;
using Forest.OutServices.Service;
using Forest.OutServices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        IContract service;
        public StyleController()
        {
            service = new Service();
        }
        // GET: api/<StyleController>
        [HttpGet]
        public async Task<IActionResult>Get()
        {
            Style[] styles = service.GetRecordStyles();
            if(styles== null)
                return StatusCode (StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, styles);
        }

        

        // GET api/<StyleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StyleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StyleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        // DELETE api/<StyleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
