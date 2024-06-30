using Microsoft.AspNetCore.Mvc;
using Comati3.Models;

using Microsoft.EntityFrameworkCore;
using Comati3.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comati3.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        readonly ComatiContext _comatiContext;
        public PersonController(ComatiContext comatiContext)
        {
            _comatiContext = comatiContext;
        }
        // POST api/<PersonController>
        [HttpPost]
        public string Post([FromBody] PersonPostDTO person)
        {
            
            Person p = person.ToModel<Person>();
            _comatiContext.Persons.Add(p);
            _comatiContext.SaveChanges();

            return "Person Added Successfully";
        }
        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public PersonPostDTO Get(int id)
        {
            PersonPostDTO p = _comatiContext.Persons.Where(person => person.Id == id).Select(person => new PersonPostDTO
            {
                Name = person.Name,
                Address = person.Address,
                Phone = person.Phone,
                Remarks = person.Remarks
            }).First();
            return p;
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _comatiContext.Persons.Find(id).IsDeleted= true;
            _comatiContext.SaveChanges();
        }
    }
}
