using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Comati3.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comati3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComatiMemberController : ControllerBase
    {
        private readonly ComatiContext _comatiContext;
        public ComatiMemberController(ComatiContext comatiContext)
        {
            _comatiContext = comatiContext;
        }
        // POST api/<PersonController>
        [HttpPost]
        public IActionResult Post([FromBody] ComatiMemberPostDTO comatiMember)
        {
            
            ComatiMember cm = comatiMember.ToModel<ComatiMember>();
            _comatiContext.Members.Add(cm);
            _comatiContext.SaveChanges();

            return Ok("Comati Member Added");
        }
        // GET: api/<ComatiMemberController>
        //[HttpGet]
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
*/
        // GET api/<ComatiMemberController>/5
        
        [HttpGet("{id}")]
        public ComatiMemberPostDTO Get(int id)
        {
            ComatiMemberPostDTO cp = _comatiContext.Members.Where(comatiMember => comatiMember.Id == id).Select(comatiMember => new ComatiMemberPostDTO
            {
                ComatiId = comatiMember.ComatiId,
                PersonId = comatiMember.PersonId,
                Amount = comatiMember.Amount,
                
        }).First();
            
            return cp;
        }

        /*// PUT api/<ComatiMemberController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE api/<ComatiMemberController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _comatiContext.Members.Find(id).IsDeleted= true;
        }
    }
}
