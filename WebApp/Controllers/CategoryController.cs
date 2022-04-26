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
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category, int> repository;

        public CategoryController(IRepository<Category, int> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return repository.GetAll();
        }

        [HttpGet("id")]
        public ActionResult<Category> Get(int id)
        {
            var category = repository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Post([FromBody]  Category category)
        {
            var createdCitation = repository.Create(category);
            if (createdCitation == null)
            {
                return BadRequest();
            }
            return createdCitation;
        }

        [HttpPut("id")]
        public ActionResult Put(int id, [FromBody] Category category)
        {
            if (!repository.Exists(id))
            {
                return NotFound();
            }
            if (id != category.Id)
            {
                return BadRequest("Id категории неверный");
            }
            bool result = repository.Update(category);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            bool result = repository.Delete(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
