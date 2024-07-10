using Microsoft.AspNetCore.Mvc;
using Comati3.Models;
using Comati3.DTOs;
using Microsoft.EntityFrameworkCore;

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
            return AddOrUpdatePerson(person);


        }
        private IActionResult AddOrUpdatePerson(PersonPostDTO person)
        {
            if (person.Id == null || person.Id==0)
            {
                Person p = person.ToModel<Person>();
                _comatiContext.Persons.Add(p);
                _comatiContext.SaveChanges();
                return Ok(p);
            }
            else
            {
                Person p = person.ToModel<Person>();
                _comatiContext.Persons.Update(p);
                _comatiContext.SaveChanges();
                return Ok(p);
            }

        } 
        // GET: api/<PersonController>
        public IEnumerable<PersonsGetDTO> GetPersons()
        {
            IEnumerable<PersonsGetDTO> p = _comatiContext.Persons.Select(person => new PersonsGetDTO
            {
                Id = person.Id,
                Name = person.Name,
                Phone= person.Phone,
                Address= person.Address,
                Remarks= person.Remarks,
                
            });
            return p;
        }
        [HttpPost("personUpdate")]
        public IActionResult Update(PersonPostDTO person)
        {
            return AddOrUpdatePerson(person);
        }

        // GET api/<PersonController>/5
        [HttpGet("personId")]
        public PersonPostDTO Get(int id)
        { if (id == 0) { return null; }
            else
            {
                if (_comatiContext.Persons != null)
                {
                    PersonPostDTO p = _comatiContext.Persons.Where(person => person.Id == id).Select(person => new PersonPostDTO
                    {
                        Name = person.Name,
                        Address = person.Address,
                        Phone = person.Phone,
                        Remarks = person.Remarks,
                    }).First();
                    return p;
                } return null;
            }
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
