using Microsoft.AspNetCore.Mvc;
using Comati3.Models;
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
        public IActionResult Post([FromBody] PersonPostDTO person)
        {
            
            Person p = person.ToModel<Person>();
            _comatiContext.Persons.Add(p);
            _comatiContext.SaveChanges();

            return Ok(p);
        }
        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<PersonsGetDTO> GetPersons()
        {
            IEnumerable<PersonsGetDTO> p = _comatiContext.Persons.Select(person => new PersonsGetDTO
            {
                Id = person.Id,
                Name = person.Name
            });
            return p;
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

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _comatiContext.Persons.Find(id).IsDeleted= true;
            _comatiContext.SaveChanges();
        }
    }
}
