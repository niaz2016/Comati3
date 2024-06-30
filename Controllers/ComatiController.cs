using Comati3.DTOs;
using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comati3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComatiController : ControllerBase
    {
        readonly ComatiContext _comatiContext;

        public ComatiController(ComatiContext comatiContext)
        {
            _comatiContext = comatiContext;
        }
        // POST api/<ComatiController>
        [HttpPost]
        public string Post([FromBody] ComatiPostDTO comati)
        {
            Comati c = comati.ToModel<Comati>();
            _comatiContext.Comaties.Add(c);
            _comatiContext.SaveChanges();

            return "Comati created Successfully";
        }
        // GET: api/<ComatiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ComatiController>/ id is comati id.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var c = _comatiContext.Comaties.Include(y=>y.Members).Where(comati => comati.Id == id).Select(comati => new
            {

                Name = comati.Name,
                ManagerId = comati.ManagerId,
                Per_Head = comati.Per_Head,
                Start_Date = comati.Start_Date,
                End_Date = comati.End_Date,
                totalMembers = comati.Members.Count(),
                totalCommati= comati.Members.Sum(member=>member.Amount)

            }).First();
            return Ok(c);
        }

        /*// PUT api/<ComatiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE api/<ComatiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _comatiContext.Comaties.Find(id).IsDeleted = true;
            _comatiContext.SaveChanges() ;
        }
    }
}
