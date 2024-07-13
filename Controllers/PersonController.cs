﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        private IActionResult AddOrUpdatePerson(PersonPostDTO person)
        {
            Person p = person.ToModel<Person>();
            if (person.Id == null || person.Id==0)
            {
                
                _comatiContext.Persons.Add(p);
                _comatiContext.SaveChanges();
                return Ok(p);
            }
            else
            {
                _comatiContext.Persons.Update(p);
                _comatiContext.SaveChanges();
                return Ok(p);
            }

        }
        // GET: api/<PersonController>
        [HttpGet]
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
        public PersonPostDTO GetPersonById(int id)
        {
            PersonPostDTO p = _comatiContext.Persons.Where(p=> p.Id == id && p.IsDeleted == false).Select(newP =>new PersonPostDTO
            {
                Name = newP.Name,
                Phone = newP.Phone,
                Address = newP.Address,
                Remarks = newP.Remarks,
            }).FirstOrDefault();
            return p;
        }

        // DELETE api/<PersonController>/5
        [HttpDelete]
         public IActionResult Delete(int id)
        {
            _comatiContext.Persons.Find(id).IsDeleted= true;
            _comatiContext.SaveChanges();
            return Ok("Delete Success");
        }
    }
}
