using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Model;
using WebApp.Data.Repos;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitationsController : ControllerBase
    {
        private readonly IRepository<Citation, int> repository;

        public CitationsController(IRepository<Citation, int> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<Citation> GetAll()
        {
            return repository.GetAll();
        }

        [HttpGet("id")]
        public ActionResult<Citation> Get(int id)
        {
            var citation = repository.Get(id);
            if (citation == null)
            {
                return NotFound();
            }
            return citation;
        }

        [HttpPost]
        public ActionResult<Citation> Post([FromBody]  Citation citation)
        {
            var createdCitation = repository.Create(citation);
            if (createdCitation == null)
            {
                return BadRequest();
            }
            return createdCitation;
        }

        [HttpPut("id")]
        public ActionResult Put(int id, [FromBody] Citation citation)
        {
            if (!repository.Exists(id))
            {
                return NotFound();
            }
            if (id != citation.Id)
            {
                return BadRequest();
            }
            bool result = repository.Update(citation);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            if (!repository.Exists(id))
            {
                return NotFound();
            }
            bool result = repository.Delete(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
