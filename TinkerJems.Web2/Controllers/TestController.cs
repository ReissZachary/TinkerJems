using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;

namespace TinkerJems.Web2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IJewelryRepository _repository;
        private IMapper _mapper;

        public TestController(IJewelryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public Task<ActionResult<JewelryItem>> Get(string name)
        {
            try
            {
                var items = _repository.GetJewelryItemByName(name);
                return _mapper.Map<JewelryItem>(items);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database error");
            }
        }

        // GET: api/detail?id=x
        [HttpGet("{name}")]
        
        // GET: api/Test
        [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"value: {id}";
        }

        // POST: api/Test
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/TestWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
